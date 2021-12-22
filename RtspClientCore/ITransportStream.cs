using System;

namespace RtspClientCore
{
    interface ITransportStream
    {
        void Process(ArraySegment<byte> payloadSegment);
    }
}