using OpenCvSharp;

namespace IPCameraClient
{

    public class VideoStreamConversion : IDisposable
    {
        private CancellationTokenSource RecordcancellationToken;
        private int streamWidth;
        private int streamHeight;
        private string urlToCamera;

        public bool IsRecording { get; internal set; }

        public VideoStreamConversion(string url, int streamWidth, int streamHeight)
        {
            this.streamWidth = streamWidth;
            this.streamHeight = streamHeight;
            urlToCamera = url;
        }

        public async Task RecordStream(string dir, string filename)
        {

            RecordcancellationToken = new CancellationTokenSource();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var _recPath = Path.Combine(dir, filename + ".mp4");
            await TakeVideo(_recPath, RecordcancellationToken.Token);

        }

        private async Task TakeVideo(string filename, CancellationToken token)
        {
            await Task.Run(() => OpencvRecord(filename), token);
        }

        private void OpencvRecord(string filename)
        {
            OpenCvSharp.Size dsize = new Size(streamWidth, streamHeight);
            using (VideoCapture capture = new VideoCapture(urlToCamera))
            using (VideoWriter writer = new VideoWriter(filename, FourCC.H264, capture.Fps, dsize))
            {
                Mat frame = new Mat();
                Console.WriteLine("Recording...");
                while (!RecordcancellationToken.IsCancellationRequested)
                {
                    IsRecording = true;
                    Mat image = LoadImage();
                    capture.Read(frame);
                    if (!frame.Empty())
                    {
                        PutImage(frame, image, 100, 100);
                        Cv2.PutText(frame,
                                        "Copyright © 2022 Eram",
                                        new OpenCvSharp.Point(frame.Width - (frame.Width - frame.Width / 4), (frame.Height - frame.Height / 6)),
                                        HersheyFonts.HersheyComplex,
                                        1.3,
                                        new Scalar(255, 255, 255),
                                        2);


                        writer.Write(frame);
                    }
                    else
                        break;
                }
                frame.Dispose();
                IsRecording = false;
                Console.WriteLine($"\n Stop Recording... File : {filename}");
            }
        }


        private static Mat LoadImage()
        {
            var smallerpic = OpenCvSharp.Cv2.ImRead(@"D:\Projects\Dotnet\DotnetProjects\SecuritySystem\UIService\wwwroot\logo.png", ImreadModes.Unchanged);
            Cv2.CvtColor(smallerpic, smallerpic, ColorConversionCodes.BGR2BGRA);
            Cv2.Resize(smallerpic, smallerpic, new Size(100, 100));
            return smallerpic;
        }

        private void PutImage(Mat frame, Mat logo, int xPos, int yPos)
        {

            var ColorChannels = Cv2.Split(logo);
            var RGBChannel = new Mat[]{ ColorChannels[0], ColorChannels[1], ColorChannels[2] };
            var mask = ColorChannels[3]; // Alpha Channel
            Cv2.Merge(RGBChannel, logo);
            logo.CopyTo(frame.RowRange(yPos, yPos + logo.Rows).ColRange(xPos, xPos + logo.Cols), mask);
        }

        public void Dispose()
        {
            if (RecordcancellationToken != null)
            {
                RecordcancellationToken.Cancel();

            }
        }
    }
}
