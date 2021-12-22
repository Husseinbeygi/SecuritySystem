using System;

namespace RtspClientCore.RawFrames.Audio
{
    public abstract class RawAudioFrame : RawFrame
    {
        public override FrameType Type => FrameType.Audio;

        protected RawAudioFrame(DateTime timestamp, ArraySegment<byte> frameSegment)
            : base(timestamp, frameSegment)
        {
        }
    }
}