using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using RemoteControlV3.Commands;
using RemoteControlV3.Connection;
using RemoteControlV3.Logging;
using RemoteControlV3.Utils;

namespace RemoteControlV3
{
    public static class Program
    {
        public static Configuration Config;

        public static Logger Logger;

        public static CommandHandler CommandHandler;

        public static SerialConnection Connection;
        
        public static TextWriter ConnectionWriter;

        public static Platform Platform;

        public static PermissionLevel PermissionLevel;
        
        static void Main(string[] args)
        {
            Config = new Configuration();
            if (File.Exists("config.bin"))
            {
                Config = Configuration.FromFile("config.bin");
            }
            else
            {
                Config.Save("config.bin");
            }
            Logger = new Logger();
            Logger.Log("Application Started!");
            try
            {
                Connection = new SerialConnection(Config.ConnectionSettings);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                #if DEBUG
                ConnectionWriter = Console.Out;     
                #endif
            }
            CommandHandler = new CommandHandler();
            Platform = GetPlatform();
            PermissionLevel = SecurityUtils.GetPermissionLevel();
            while (true)
            {
                CommandHandler.Parse(Console.ReadLine());
                System.Threading.Thread.Sleep(100);
            }
            Logger.Flush();
        }

        private static Platform GetPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Platform.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Platform.Windows;
            }

            return Platform.Unknown;
        }
    }
}
