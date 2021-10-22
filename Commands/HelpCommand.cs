using System;
using System.Reflection;
using System.Text;

namespace RemoteControlV3.Commands
{
    [Command("Help")]
    [CommandHelp("Retrieves these help messages", "Retrieves help messages.\nUsage:\nParameterless - Lists all commands\nCommand as a parameter - Gets the extended help message like this")]
    public class HelpCommand : Command
    {
        public override void Run(string[] args)
        {
            if (args.Length == 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("List of all commands");
                foreach (Type t in Program.CommandHandler.Commands)
                {
                    string description = "This command has no description";
                    CommandHelpAttribute helpAttribute = t.GetCustomAttribute<CommandHelpAttribute>();
                    if (helpAttribute != null)
                    {
                        description = helpAttribute.CommandDescription;
                    }
                    CommandAttribute attribute = t.GetCustomAttribute<CommandAttribute>();
                    builder.AppendLine($"{attribute.Name} - {description}");
                }
                Program.ConnectionWriter.Write(builder.ToString());
            }
            else
            {
                string name = args[0];
                foreach (Type t in Program.CommandHandler.Commands)
                {
                    CommandAttribute attribute = t.GetCustomAttribute<CommandAttribute>();
                    if (attribute.Name.Equals(name))
                    {
                        string helpMessage = "This command has no help message";
                        CommandHelpAttribute helpAttribute = t.GetCustomAttribute<CommandHelpAttribute>();
                        if (helpAttribute != null && !string.IsNullOrEmpty(helpAttribute.HelpMessage))
                        {
                            helpMessage = helpAttribute.HelpMessage;
                        }
                        else if (helpAttribute != null)
                        {
                            helpMessage = helpAttribute.CommandDescription + "\nThis command has no extended help message";
                        }
                        Program.ConnectionWriter.WriteLine($"Help for command {attribute.Name}:\n{helpMessage}");
                        return;
                    }
                }
                Program.ConnectionWriter.WriteLine("No command found");
            }
        }
    }
}