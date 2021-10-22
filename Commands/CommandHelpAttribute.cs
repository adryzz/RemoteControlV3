using System;

namespace RemoteControlV3.Commands
{
    [AttributeUsage(AttributeTargets.Class)]  
    public class CommandHelpAttribute : Attribute
    {
        public string CommandDescription;
        public string HelpMessage;
        public CommandHelpAttribute(string commandDescription, string helpMessage = "")
        {
            CommandDescription = commandDescription;
            HelpMessage = helpMessage;
        }
    }
}