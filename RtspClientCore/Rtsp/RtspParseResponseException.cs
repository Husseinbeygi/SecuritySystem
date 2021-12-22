using System;

namespace RtspClientCore.Rtsp
{
    [Serializable]
    public class RtspParseResponseException : RtspClientException
    {
        public RtspParseResponseException(string message) : base(message)
        {
        }
    }
}