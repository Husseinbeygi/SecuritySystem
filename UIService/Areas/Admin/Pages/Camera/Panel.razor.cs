﻿using FrameDecoderCore;
using IPCameraClient;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RtspClientCore;
using RtspClientCore.RawFrames;
using RtspClientCore.RawFrames.Video;
using RtspClientCore.Rtsp;
using SecurityService.Application.Service.Dtos.Camera.IPCamera;
using System.Drawing;

namespace UIService.Areas.Admin.Pages.Camera
{
    public partial class Panel
    {

        public Panel() { }

        [Parameter]
        public long id { get; set; }
        public EditIPCamera cam { get; set; } = new();
        private string urlToCamera;
        private const int streamWidth = 1280;
        private const int streamHeight = 720;

        private static readonly FrameDecoderCore.FrameDecoder FrameDecoder = new FrameDecoderCore.FrameDecoder();
        private static readonly FrameDecoderCore.FrameTransformer FrameTransformer = new FrameDecoderCore.FrameTransformer(streamWidth, streamHeight);
        private Task? connectTask;
        private CancellationTokenSource cancellationTokenSource;
        private byte[] ByteData = { };
        private Bitmap bitmapFrame;
        VideoStreamConversion videoStream;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            cam = _camApplication.GetDetails(id);
            var camUrl = _rtspgenerator.GenerateUrl(cam.HostAddress, cam.UserName, cam.Password, cam.StreamAddress);
            var rootPath = _webHostEnvironment.WebRootPath;
            urlToCamera = camUrl;
            videoStream = new(camUrl, streamWidth, streamHeight);

            var serverUri = new Uri(urlToCamera);
            //var credentials = new NetworkCredential("admin", "admin12345678");

            var connectionParameters = new ConnectionParameters(serverUri/*, credentials*/);
            ConnectToCamera(connectionParameters);

        }



        private void ConnectToCamera(ConnectionParameters connectionParameters)
        {
            cancellationTokenSource = new CancellationTokenSource();

            connectTask = ConnectAsync(connectionParameters, cancellationTokenSource.Token);


        }

        private async Task ConnectAsync(ConnectionParameters connectionParameters, CancellationToken token)
        {
            try
            {
                var delay = TimeSpan.FromSeconds(5);

                using (var rtspClient = new RtspClient(connectionParameters))
                {
                    rtspClient.FrameReceived += RtspClient_FrameReceived;

                    while (true)
                    {
                        try
                        {

                            Console.WriteLine("Connecting...");
                            await rtspClient.ConnectAsync(token);

                            Console.WriteLine("Connected.");
                            await rtspClient.ReceiveAsync(token);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        catch (RtspClientException e)
                        {
                            Console.WriteLine(e.ToString());
                            await Task.Delay(delay, token);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void RtspClient_FrameReceived(object sender, RawFrame rawFrame)
        {
            if (!(rawFrame is RawVideoFrame rawVideoFrame))
                return;

            var decodedFrame = FrameDecoder.TryDecode(rawVideoFrame);

            if (decodedFrame == null)
                return;

            bitmapFrame = FrameTransformer.TransformToBitmap(decodedFrame);
            ShowFrameInPage();
            StateHasChanged();

        }
        private void ShowFrameInPage()
        {
            using MemoryStream stream = new MemoryStream();
            bitmapFrame.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ByteData = stream.ToArray();
        }

        public void TakeImage()
        {
            Console.WriteLine("TakeImage!");
            var filename = DateTime.Now.ToString();
            var a = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ByteData));
            jsruntime.InvokeVoidAsync("downloadFile", "image/jpeg", a, filename);

        }

        public async Task TakeVideoAsync()
        {
            if (videoStream.IsRecording)
            {
                videoStream.Dispose();

            }
            else
            {
                using (videoStream)
                {
                    await videoStream.RecordStream();
                }
            }
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            Console.WriteLine("Canceling");
            connectTask.WaitAsync(CancellationToken.None);

        }



    }
}