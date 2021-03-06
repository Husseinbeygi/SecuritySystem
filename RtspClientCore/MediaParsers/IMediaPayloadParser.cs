using System;
using RtspClientCore.RawFrames;

namespace RtspClientCore.MediaParsers
{
    interface IMediaPayloadParser
    {
        DateTime BaseTime { get; set; }

        Action<RawFrame> FrameGenerated { get; set; }

        void Parse(TimeSpan timeOffset, ArraySegment<byte> byteSegment, bool markerBit);

        void ResetState();
    }
}