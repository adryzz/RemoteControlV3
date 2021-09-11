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
            //get time and round it up to the nearest second
            Instant now = Instant.FromUnixTimeSeconds(SystemClock.Instance.GetCurrentInstant().ToUnixTimeSeconds());
            int secret = (now.ToUnixTimeSeconds() + Program.Config.LogSecret).GetHashCode();
            byte[] encoded = BinaryUtils.Encode(text, secret);
            Message = Convert.ToBase64String(encoded);
            LogTime = now;
            Type = type;
            Severity = severity;
        }

        public override string ToString()
        {
            return $"[{Severity.ToString().ToUpper()}] | [{Type.ToString().ToUpper()}] | {LogTime} | {Message}\n";
        }
    }
}
