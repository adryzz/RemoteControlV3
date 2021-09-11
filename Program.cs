using System;
using RemoteControlV3.Logging;

namespace RemoteControlV3
{
    public static class Program
    {
        public static Configuration Config;

        public static Logger Logger;

        static void Main(string[] args)
        {
            Config = new Configuration();
            if (System.IO.File.Exists("config.bin"))
            {
                Config = Configuration.FromFile("config.bin");
            }
            Logger = new Logger();
            Logger.Log("Application Started!");
            System.Threading.Thread.Sleep(5000);
            Logger.Flush();
        }
    }
}
