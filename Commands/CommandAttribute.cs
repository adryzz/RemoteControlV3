using System;

namespace RemoteControlV3.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]  
    public class CommandAttribute : Attribute  
    {  
        public string Name;
  
        public CommandAttribute(string name)  
        {  
            Name = name;
        }  
    }
}
