using System;
using RtspClientCore.RawFrames;

namespace RtspClientCore.MediaParsers
{
    interface IMediaPayloadParser
    {
        Action<RawFrame> FrameGenerated { get; set; }

        void Parse(TimeSpan timeOffset, ArraySegment<byte> byteSegment, bool markerBit);

        void ResetState();
    }
}