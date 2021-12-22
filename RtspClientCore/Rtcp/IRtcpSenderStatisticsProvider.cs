using System;

namespace RtspClientCore.Rtcp
{
    interface IRtcpSenderStatisticsProvider
    {
        DateTime LastTimeReportReceived { get; }
        long LastNtpTimeReportReceived { get; }
    }
}