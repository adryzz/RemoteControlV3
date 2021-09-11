using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RemoteControlV3.Logging;
using RemoteControlV3.Utils;

namespace RemoteControlV3
{
    public class Configuration
    {
        private const int SHARED_SECRET = 542382837;

        public bool UseSecureLogging { get; set; } = true;

        public int LogSecret { get; set; } = SHARED_SECRET;

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        public static Configuration FromFile(string fileName)
        {
            byte[] encoded = File.ReadAllBytes(fileName);
            string decoded = BinaryUtils.Decode(encoded, SHARED_SECRET);
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(decoded));
            return FromPlainText(json);
        }

        public static Configuration FromPlainText(string json)
        {
            return JsonConvert.DeserializeObject<Configuration>(json);
        }

        public void Save(string fileName)
        {
            string json = JsonConvert.SerializeObject(this);
            string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            byte[] encoded = BinaryUtils.Encode(text, SHARED_SECRET);
            File.WriteAllBytes(fileName, encoded);
        }

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
}