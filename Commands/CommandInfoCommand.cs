using System;
using System.Reflection;
using System.Text;

namespace RemoteControlV3.Commands
{
    [Command("CommandInfo")]
    [CommandHelp("Displays information about a specific command")]
    public class CommandInfoCommand : Command
    {
        public override void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Program.ConnectionWriter.WriteLine("Too little parameters");
                throw new ArgumentException();
            }

            string name = args[0];
            foreach (Type t in Program.CommandHandler.Commands)
            {
                CommandAttribute attribute = t.GetCustomAttribute<CommandAttribute>();
                if (attribute.Name.Equals(name))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("Info about the command " + name + ":");
                    bool builtin = t.Assembly.GetHashCode() == Assembly.GetEntryAssembly().GetHashCode();
                    builder.AppendLine("Source: " + (builtin ? "Built-in" : ("External - " + t.Assembly.FullName)));
                    builder.AppendLine("Supported platforms: " + attribute.Platform);
                    builder.AppendLine("Required permission level: " + attribute.PermissionLevel);
                    Program.ConnectionWriter.Write(builder.ToString());
                    return;
                }
            }
        }
    }
}