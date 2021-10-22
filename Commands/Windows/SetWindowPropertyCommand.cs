using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Commands.Windows
{
    [Command("Set-WindowProperty", Platform.Windows)]
    [CommandHelp("Sets various properties to windows", "Properties:\nEnabled - Visible - Opacity - TopMost - Title")]
    public class SetWindowPropertyCommand : Command
    {
        public override void Run(string[] args)
        {
            if (args.Length < 2)
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

            switch(args[0])
            {
                case "Enabled":
                    {
                        bool? value = Utils.ParseUtils.ParseBool(args[1]);

                        if (value.HasValue)
                        {
                            window.Enabled = value.Value;
                            Program.ConnectionWriter.WriteLine("Done!");
                            return;
                        }
                        break;
                    }
                case "Visible":
                    {
                        bool? value = Utils.ParseUtils.ParseBool(args[1]);

                        if (value.HasValue)
                        {
                            window.Visible = value.Value;
                            Program.ConnectionWriter.WriteLine("Done!");
                            return;
                        }
                        break;
                    }
                case "Opacity":
                    {
                        byte value;
                        if (byte.TryParse(args[1], out value))
                        {
                            window.Opacity = value;
                            Program.ConnectionWriter.WriteLine("Done!");
                            return;
                        }
                        break;
                    }
                case "Title":
                    {
                        string title = "";
                        for(int i = 1; i < args.Length; i++)
                        {
                            title += args[i] + " ";
                        }
                        title.TrimEnd();
                        window.Title = title;
                        Program.ConnectionWriter.WriteLine("Done!");
                        return;
                    }
                case "TopMost":
                    {
                        bool? value = Utils.ParseUtils.ParseBool(args[1]);

                        if (value.HasValue)
                        {
                            window.TopMost = value.Value;
                            Program.ConnectionWriter.WriteLine("Done!");
                            return;
                        }
                        break;
                    }
                /*case "Size":
                        {
                            Size? value = Utils.ParseUtils.ParseSize(args[1]);

                            if (value.HasValue)
                            {
                                window.s = value.Value;
                                Program.ConnectionWriter.WriteLine("Done!");
                                return;
                            }
                            break;
                            return;
                        }*/
            }
            Program.ConnectionWriter.WriteLine("Invalid property/state");
        }
    }
}
