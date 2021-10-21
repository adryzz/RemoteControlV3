using System;

namespace RemoteControlV3.Commands
{
    [Command("Exit")]
    [CommandHelp("Exits the application")]
    public class ExitCommand : Command
    {
        public override void Run(string[] args)
        {
            Environment.Exit(0);
        }
    }
}