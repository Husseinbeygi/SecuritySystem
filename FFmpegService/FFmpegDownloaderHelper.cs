using Xabe.FFmpeg.Downloader;
namespace FFmpegService
{
    public static class FFmpegDownloaderHelper
    {
        public static Task DownloadTheFFmpegOfficial()
        {
            return FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
        }

    }
}
