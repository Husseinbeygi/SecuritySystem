using RtspClientCore.Utils;

namespace RtspClientCore.Rtp
{
    internal interface IRtpSequenceAssembler
    {
        RefAction<RtpPacket> PacketPassed { get; set; }

        void ProcessPacket(ref RtpPacket rtpPacket);
    }
}