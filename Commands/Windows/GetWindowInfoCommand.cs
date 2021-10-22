using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Commands.Windows
{
    [Command("Get-WindowInfo", Platform.Windows)]
    [CommandHelp("Gets information about a window")]
    public class GetWindowInfoCommand : Command
    {
        public override void Run(string[] args)
        {
            if (!(Program.Memory is Types.Window))
            {
                Program.ConnectionWriter.WriteLine("No window in memory");
                return;
            }

            Types.Window window = (Types.Window)Program.Memory;

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Title: \"" + window.Title + "\"");
            builder.AppendLine("Visible: " + window.Visible);
            builder.AppendLine("Opacity: " + window.Opacity + "/255");
            builder.AppendLine("TopMost: " + window.TopMost);
            builder.AppendLine("Enabled: " + window.Enabled);
            builder.AppendLine("Handle: " + window.Handle);

            Program.ConnectionWriter.Write(builder.ToString());
        }
    }
}
