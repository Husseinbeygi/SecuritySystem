using System;

namespace RtspClientCore.Rtsp
{
    [Serializable]
    public class RtspBadResponseException : RtspClientException
    {
        public RtspBadResponseException(string message) : base(message)
        {
        }
    }
}