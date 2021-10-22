using System;

namespace RemoteControlV3.Commands
{
    [Command("Set-Port")]
    [CommandHelp("Changes the port in the program config")]
    public class SetPortCommand : Command

    {
        public override void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Program.ConnectionWriter.WriteLine("Too little parameters");
                throw new ArgumentException();
            }
            else if (args.Length == 1)
            {
                Program.Config.ConnectionSettings.Port = args[0];
            }
            else if (args.Length >= 2)
            {
                Program.Config.ConnectionSettings.Port = args[0];
                Program.Config.ConnectionSettings.BaudRate = int.Parse(args[1]);
            }
            Program.Config.Save("config.bin");
            Program.ConnectionWriter.WriteLine("Done!");
        }
    }
}