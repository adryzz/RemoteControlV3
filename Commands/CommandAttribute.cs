using System;
using RemoteControlV3;

namespace RemoteControlV3.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]  
    public class CommandAttribute : Attribute  
    {  
        public string Name;
        public Platform Platform;
        public PermissionLevel PermissionLevel;
        public CommandAttribute(string name, Platform platform = Platform.All, PermissionLevel permissionLevel = PermissionLevel.User)  
        {  
            Name = name;
            Platform = platform;
            PermissionLevel = permissionLevel;
        }  
    }
}
