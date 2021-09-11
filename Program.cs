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
            Logger = new Logger();
            Logger.Log("Application Started!");
            Logger.Flush();
        }
    }
}
