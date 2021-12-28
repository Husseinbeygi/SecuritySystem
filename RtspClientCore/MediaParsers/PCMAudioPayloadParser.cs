using RtspClientCore.Codecs.Audio;
using RtspClientCore.RawFrames.Audio;
using System;

namespace RtspClientCore.MediaParsers
{
    class PCMAudioPayloadParser : MediaPayloadParser
    {
        private readonly PCMCodecInfo _pcmCodecInfo;

        public PCMAudioPayloadParser(PCMCodecInfo pcmCodecInfo)
        {
            _pcmCodecInfo = pcmCodecInfo ?? throw new ArgumentNullException(nameof(pcmCodecInfo));
        }

        public override void Parse(TimeSpan timeOffset, ArraySegment<byte> byteSegment, bool markerBit)
        {
            DateTime timestamp = GetFrameTimestamp(timeOffset);

            var frame = new RawPCMFrame(timestamp, byteSegment, _pcmCodecInfo.SampleRate, _pcmCodecInfo.BitsPerSample,
                _pcmCodecInfo.Channels);

            OnFrameGenerated(frame);
        }

        public override void ResetState()
        {
        }
    }
}