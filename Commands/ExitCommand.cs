using System;

namespace RemoteControlV3.Commands
{
    [Command("Exit")]
    public class ExitCommand : Command
    {
        public override void Run(string[] args)
        {
            Environment.Exit(0);
        }
    }
}