namespace FFmpegService
{
    public static class FFmpegVideo
    {
        public async static void TakeVideo(string IpCameraRtspUrl, string FilePath)
        {

            if (FFmpegDownloaderHelper.DownloadTheFFmpegOfficial().Status == TaskStatus.RanToCompletion)
            {

                var mediaInfo = IpCameraRtspUrl;
                System.Diagnostics.Process process = new();
                System.Diagnostics.ProcessStartInfo startInfo = new();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C ffmpeg -i {mediaInfo} -ss 00:00:0.0 -t 15 -an {FilePath}/record2.mp4";
                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                Console.WriteLine("Could not resolve FFmpeg!");
            }
        }

    }
}