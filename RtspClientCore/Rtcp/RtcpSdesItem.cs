using System.IO;

namespace RtspClientCore.Rtcp
{
    abstract class RtcpSdesItem
    {
        public abstract int SerializedLength { get; }

        public abstract void Serialize(Stream stream);
    }
}