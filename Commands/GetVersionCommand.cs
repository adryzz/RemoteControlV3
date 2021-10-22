using System;
using System.Reflection;
using RemoteControlV3.Logging;

namespace RemoteControlV3.Commands
{
    [Command("Get-Version")]
    public class GetVersionCommand : Command
    {
        public override void Run(string[] args)
        {
            Program.ConnectionWriter.WriteLine(Assembly.GetEntryAssembly().GetName().Version);
        }
    }
}
