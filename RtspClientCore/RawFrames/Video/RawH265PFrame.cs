using System;

namespace RtspClientCore.RawFrames.Video
{
    public class RawH265PFrame : RawH265Frame
    {
        public RawH265PFrame(DateTime timestamp, ArraySegment<byte> frameSegment) :
           base(timestamp, frameSegment)
        {
        }
    }
}
