namespace FFmpegService
{
    public class FFmpegStream
    {

        public System.Diagnostics.Process StreamVideo(string IpCameraRTSPUrl,string outputPath)
        {
            if (FFmpegDownloaderHelper.DownloadTheFFmpegOfficial().Status == TaskStatus.RanToCompletion)
            {
                string mediaInfo = IpCameraRTSPUrl;
                System.Diagnostics.Process process = new();
                System.Diagnostics.ProcessStartInfo startInfo = new();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "ffmpeg.exe"; //for linux /bin/bash

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }
                startInfo.Arguments = $"-i {mediaInfo} -g 25 -sc_threshold 0 -codec copy -preset ultrafast -flags -global_header -map 0 -segment_list_flags +live -segment_time 1 -hls_list_size 1 -hls_time 1 -f segment  -segment_list {outputPath}/playlist.m3u8   {outputPath}/out%03d.ts";

                process.StartInfo = startInfo;

                return process;
            }
            else
            {
                FFmpegDownloaderHelper.DownloadTheFFmpegOfficial();
                return null;
            }
        }

    }
}