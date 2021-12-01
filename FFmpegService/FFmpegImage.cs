namespace FFmpegService
{
    public static class FFmpegImage
    {

        public static void TakeImage(string IpCameraRtspUrl,string Outputpath)
        {

            if (FFmpegDownloaderHelper.DownloadTheFFmpegOfficial().Status == TaskStatus.RanToCompletion)
            {
                var mediaInfo = IpCameraRtspUrl;
                string filename = Outputpath + "Img.jpg";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C ffmpeg.exe -y -i {mediaInfo} -vframes 1 {Outputpath}/Img.jpg";
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
