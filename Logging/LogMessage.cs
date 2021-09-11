using System;
using NodaTime;
namespace RemoteControlV3.Logging
{
    public class LogMessage
    {
        public Instant LogTime;

        public LogType Type;

        public LogLevel Severity;

        public string Message;

        public LogMessage(string text)
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();

        }
    }
}
