using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Commands.Windows
{
    [Command("Get-Window", Platform.Windows)]
    [CommandHelp("Grabs the handle of the current window", "Usage:\nParameterless: Get active window\nWith parameters: Get the window with a specific title")]
    public class GetWindowCommand : Command
    {
        public override void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Program.Memory = Types.Window.GetActiveWindow();
            }
            else
            {
                string title = "";
                foreach(string s in args)
                {
                    title += s + " ";
                }
                title.TrimEnd();
                Program.Memory = Types.Window.GetWindowFromTitle(title);
            }
            Program.ConnectionWriter.WriteLine("Grabbed a window: \"" + Program.Memory.ToString() + "\"");
        }
    }
}
