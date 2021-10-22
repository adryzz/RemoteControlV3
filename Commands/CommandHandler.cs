using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RemoteControlV3.Commands
{
    public class CommandHandler
    {
        public List<Type> Commands = new List<Type>();
        
        public CommandHandler()
        {
            listCommands();
        }

        public void Refresh()
        {
            Commands.Clear();
            listCommands();
        }
        
        private void listCommands()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic))
            {
                foreach (Type t in assembly.ExportedTypes)
                {
                    if (t.IsAssignableTo(typeof(Command)) && !t.IsAbstract)
                    {
                        bool hasName = false;
                        foreach (var attribute in t.CustomAttributes)
                        {
                            if (attribute.AttributeType.IsAssignableFrom(typeof(CommandAttribute)))
                            {
                                hasName = true;
                                break;
                            }
                        }

                        if (hasName)
                        {
                            Commands.Add(t);
                        }
                    }
                }
            }
        }

        public void Parse(string command)
        {
            string name = command;
            if (command.Contains(' '))
            {
                name = command.Remove(command.IndexOf(' '));
            }
            foreach (var t in Commands)
            {
                CommandAttribute attribute = t.GetCustomAttribute<CommandAttribute>();
                
                if (attribute != null)
                {
                    if (attribute.Name == name)
                    {
                        if (attribute.Platform == Platform.All || attribute.Platform == Program.Platform)
                        {
                            if ((int) attribute.PermissionLevel >= (int)Program.PermissionLevel)
                            {
                                run(t, command.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray());
                                return;
                            }
                            else
                            {
                                Program.ConnectionWriter.WriteLine("Missing permissions");
                                return;
                            }
                        }
                        else
                        {
                            Program.ConnectionWriter.WriteLine("Unsupported platform");
                            return;
                        }
                    }
                }
            }
            Program.ConnectionWriter.WriteLine("Unknown command");
        }

        private void run(Type t, string[] args)
        {
            try
            {
                Command command = (Command)Activator.CreateInstance(t);
                command.Run(args);
            }
            catch (Exception e)
            {
                Program.Logger.Log(e);
            }
        }
    }
}