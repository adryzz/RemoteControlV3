using System;

namespace RemoteControlV3.Commands
{
    public abstract class Command
    {
        public virtual void Run(string[] args)
        {
        }
    }
}
