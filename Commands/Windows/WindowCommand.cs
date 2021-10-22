using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Commands.Windows
{
    [Command("Window", Platform.Windows)]
    [CommandHelp("Performs various actions to windows", "Actions:\nClose - BringToFront - Maximize - Minimize")]
    public class WindowCommand : Command
    {
        public override void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Program.ConnectionWriter.WriteLine("Too little parameters");
                throw new ArgumentException();
            }

            if (!(Program.Memory is Types.Window))
            {
                Program.ConnectionWriter.WriteLine("No window in memory");
                return;
            }

            Types.Window window = (Types.Window)Program.Memory;

            switch (args[0])
            {
                case "Close":
                    {
                        window.Close();
                        Program.ConnectionWriter.WriteLine("Done!");
                        return;
                    }
                case "BringToFront":
                    {
                        window.BringToFront();
                        Program.ConnectionWriter.WriteLine("Done!");
                        return;
                    }
                case "Maximize":
                    {
                        window.Maximize();
                        Program.ConnectionWriter.WriteLine("Done!");
                        return;
                    }
                case "Minimize":
                    {
                        window.Minimize();
                        Program.ConnectionWriter.WriteLine("Done!");
                        return;
                    }
            }
            Program.ConnectionWriter.WriteLine("Invalid action");
        }
    }
}
