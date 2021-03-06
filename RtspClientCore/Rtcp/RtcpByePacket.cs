using System;
using System.Collections.Generic;
using RtspClientCore.Utils;

namespace RtspClientCore.Rtcp
{
    class RtcpByePacket : RtcpPacket
    {
        private readonly List<uint> _syncSourcesIds = new List<uint>();

        public IEnumerable<uint> SyncSourcesIds => _syncSourcesIds;

        protected override void FillFromByteSegment(ArraySegment<byte> byteSegment)
        {
            int offset = byteSegment.Offset;

            for (int i = 0; i < SourceCount; i++)
            {
                uint ssrc = BigEndianConverter.ReadUInt32(byteSegment.Array, offset);

                offset += 4;

                _syncSourcesIds.Add(ssrc);
            }
        }
    }
}