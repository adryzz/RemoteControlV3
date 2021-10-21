using System;
using System.Reflection;
using RemoteControlV3.Commands;
using RemoteControlV3.Logging;

namespace RemoteControlV3
{
    public static class Program
    {
        public static Configuration Config;

        public static Logger Logger;

        public static CommandHandler CommandHandler;

        public static Platform Platform;

        public static PermissionLevel PermissionLevel;
        
        static void Main(string[] args)
        {
            Config = new Configuration();
            if (System.IO.File.Exists("config.bin"))
            {
                Config = Configuration.FromFile("config.bin");
            }
            else
            {
                Config.Save("config.bin");
            }
            Logger = new Logger();
            Logger.Log("Application Started!");
            CommandHandler = new CommandHandler();
            while (true)
            {
                CommandHandler.Parse(Console.ReadLine());
                System.Threading.Thread.Sleep(100);
            }
            Logger.Flush();
        }
    }
}
