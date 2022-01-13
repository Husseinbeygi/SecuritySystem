using System;

namespace RtspClientCore.Codecs.Video
{
    class H265CodecInfo : VideoCodecInfo
    {
        public bool HasDonlField { get; set; }
        public byte[] VpsBytes { get; set; } = Array.Empty<byte>();

        public byte[] SpsBytes { get; set; } = Array.Empty<byte>();

        public byte[] PpsBytes { get; set; } = Array.Empty<byte>();
    }
}
