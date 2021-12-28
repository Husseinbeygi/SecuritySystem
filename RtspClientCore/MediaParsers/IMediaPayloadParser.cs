using RtspClientCore.RawFrames;
using System;

namespace RtspClientCore.MediaParsers
{
    interface IMediaPayloadParser
    {
        Action<RawFrame> FrameGenerated { get; set; }

        void Parse(TimeSpan timeOffset, ArraySegment<byte> byteSegment, bool markerBit);

        void ResetState();
    }
}