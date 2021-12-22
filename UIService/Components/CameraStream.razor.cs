using _0_Framework.Application;
using IPCameraClient;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RtspClientCore;
using RtspClientCore.RawFrames;
using RtspClientCore.RawFrames.Video;
using RtspClientCore.Rtsp;
using SecurityService.Application.Service.Dtos.Camera.IPCamera;
using System.Drawing;

namespace UIService.Components
{
    public partial class CameraStream
    {

        [Parameter]
        public EditIPCamera Camera { get; set; } = new();

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
        string CameraVideoPath;


        protected override void OnInitialized()
        {
            base.OnInitialized();
            var camUrl = _rtspgenerator.GenerateUrl(Camera.HostAddress, Camera.UserName, Camera.Password, Camera.StreamAddress);
            CameraVideoPath = Path.Combine(_webHostEnvironment.WebRootPath, "livevideos", Camera.HostAddress);
            urlToCamera = camUrl;
            videoStream = new(camUrl, streamWidth, streamHeight);

            var serverUri = new Uri(urlToCamera);

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
                    var filename = DateTime.Now.ToFarsiWithoutSlash();
                    await videoStream.RecordStream(CameraVideoPath, filename.Trim());
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
