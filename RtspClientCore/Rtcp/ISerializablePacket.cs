using System.IO;

namespace RtspClientCore.Rtcp
{
    interface ISerializablePacket
    {
        void Serialize(Stream stream);
    }
}