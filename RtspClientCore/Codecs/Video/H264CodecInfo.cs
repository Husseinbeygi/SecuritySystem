using System;

namespace RtspClientCore.Codecs.Video
{
    class H264CodecInfo : VideoCodecInfo
    {
        public byte[] SpsPpsBytes { get; set; } = Array.Empty<byte>();
    }
}