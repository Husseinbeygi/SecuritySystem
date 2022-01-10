using _0_Framework.Application;
using IPCameraClient;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RtspClientCore;
using RtspClientCore.RawFrames;
using RtspClientCore.RawFrames.Video;
using RtspClientCore.Rtsp;
using SecurityService.Application.Service.Camera.IPCamera;
using System.Drawing;

namespace UIService.Components
{
    public partial class CameraStream
    {

        [Parameter]
        public EditIPCamera Camera { get; set; } = new();
        [Parameter]
        public string FilesAddress { get; set; }

        private string urlToCamera = String.Empty;
        private const int streamWidth = 1280;
        private const int streamHeight = 720;
        private static readonly FrameDecoderCore.FrameDecoder FrameDecoder = new FrameDecoderCore.FrameDecoder();
        private static readonly FrameDecoderCore.FrameTransformer FrameTransformer = new FrameDecoderCore.FrameTransformer(streamWidth, streamHeight);
        private Task? connectTask;
        private CancellationTokenSource? cancellationTokenSource;
        private byte[] ByteData = { };
        private Bitmap? bitmapFrame;
        VideoStreamConversion? videoStream;
        string CameraVideoPath = String.Empty;
        string CameraImagePath = String.Empty;
        string boderRecord = "";
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var camUrl = _rtspgenerator.GenerateUrl(Camera.HostAddress, Camera.UserName, Camera.Password, Camera.StreamAddress);
            CameraVideoPath = Path.Combine(_webHostEnvironment.WebRootPath, Camera.HostAddress, "videos");
            CameraImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Camera.HostAddress, "pictures");
            urlToCamera = camUrl;
            videoStream = new(camUrl, streamWidth, streamHeight);

            var serverUri = new Uri(urlToCamera);

            var connectionParameters = new ConnectionParameters(serverUri/*, credentials*/);
            ConnectToCamera(connectionParameters);

        }
        public void TakeImage()
        {
            Console.WriteLine("TakeImage!");
            var filename = DateTime.Now.ToFarsiWithoutSlash();
            SaveImageOnServer(filename);
            SaveImageOnClient(filename);

        }

        public async Task TakeVideoAsync()
        {
            if (videoStream.IsRecording)
            {
                boderRecord = "";
                videoStream.Dispose();

            }
            else
            {
                using (videoStream)
                {
                    var filename = DateTime.Now.ToFarsiWithoutSlash();
                    boderRecord = "border:solid;border-color:red;border-radius:10px;";
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
            PutTextOnByteArray();
            PutImageOnByteArray();
            ConvertBitmapToShowOnHtml();
            StateHasChanged();

        }
        private void PutImageOnByteArray()
        {
            Image watermark = Image.FromFile(@"D:\Projects\Dotnet\DotnetProjects\SecuritySystem\UIService\wwwroot\logo.png");
            bitmapFrame = bitmapFrame.PutImage(watermark, 100, 100, 100, 100);
        }
        private void PutTextOnByteArray()
        {
            bitmapFrame = bitmapFrame.PutText("Copyright © 2022 Eram", new Font("TimeNewsRoman", 40, FontStyle.Bold), bitmapFrame.Width / 2, (bitmapFrame.Height - bitmapFrame.Height / 6));
        }
        private void ConvertBitmapToShowOnHtml()
        {
            using MemoryStream stream = new MemoryStream();
            bitmapFrame.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            ByteData = stream.ToArray();
        }
        private void SaveImageOnClient(string filename)
        {
            var a = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ByteData));
            jsruntime.InvokeVoidAsync("downloadFile", "image/jpeg", a, filename);
        }
        private void SaveImageOnServer(string filename)
        {
            MemoryStream stream = new MemoryStream();
            bitmapFrame.Save(PicturesPath(filename), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        private string PicturesPath(string filename)
        {
            if (Directory.Exists(CameraImagePath))
            {

                return Path.Combine(CameraImagePath, filename + ".jpg");
            }
            else
            {
                Directory.CreateDirectory(CameraImagePath);
                return Path.Combine(CameraImagePath, filename + ".jpg");

            }
        }

    }


}
