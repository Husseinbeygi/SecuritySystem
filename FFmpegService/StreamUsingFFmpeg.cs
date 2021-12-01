namespace FFmpegService
{
    public class FFmpegStream
    {

        public void StreamVideo(string IpCameraRTSPUrl,string outputPath)
        {
            if (FFmpegDownloaderHelper.DownloadTheFFmpegOfficial().Status == TaskStatus.RanToCompletion)
            {
                string mediaInfo = IpCameraRTSPUrl;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C ffmpeg  -i {mediaInfo} -g 25 -sc_threshold 0 -codec copy -preset ultrafast -flags -global_header -map 0 -segment_list_flags +live -segment_time 1 -hls_list_size 1 -hls_time 1 -f segment  -segment_list {outputPath}/playlist.m3u8   {outputPath}/out%03d.ts";

                process.StartInfo = startInfo;
                process.Start();
            }
            else
            {
                FFmpegDownloaderHelper.DownloadTheFFmpegOfficial();
            }
        }

    }
}