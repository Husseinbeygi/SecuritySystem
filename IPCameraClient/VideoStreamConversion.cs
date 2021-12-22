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

        public async Task RecordStream(string dir,string filename)
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
            using (VideoWriter writer = new VideoWriter(filename, FourCC.MP4V, capture.Fps, dsize))
            using (Mat frame = new Mat())
            {
                Console.WriteLine("Recording...");
                while (!RecordcancellationToken.IsCancellationRequested)
                {
                    IsRecording = true;
                    capture.Read(frame);
                    if (frame.Empty())
                        break;
                    Console.CursorLeft = 0;
                    Console.Write("{0} / {1}", capture.PosFrames, capture.FrameCount);
                    writer.Write(frame);
                }
                IsRecording = false;
                Console.WriteLine($"\n Stop Recording... File : {filename}");
            }
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
