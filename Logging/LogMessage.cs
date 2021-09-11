using System;
using NodaTime;
using RemoteControlV3.Utils;

namespace RemoteControlV3.Logging
{
    public class LogMessage
    {
        public Instant LogTime;

        public LogType Type;

        public LogLevel Severity;

        public string Message;

        public LogMessage(string text, LogType type = LogType.Runtime, LogLevel severity = LogLevel.Info)
        {
            Instant now = SystemClock.Instance.GetCurrentInstant();
            int secret = now.ToUnixTimeSeconds().GetHashCode();
            byte[] encoded = BinaryUtils.Encode(text, secret);
            Message = Convert.ToBase64String(encoded);
        }

        public override string ToString()
        {
            return $"[{Severity.ToString().ToUpper()}] | [{Type.ToString().ToUpper()}] | {LogTime} | {Message}\n";
        }
    }
}
