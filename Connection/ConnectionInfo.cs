using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlV3.Connection
{
    public class ConnectionInfo
    {
        public string Port { get; set; } = "COM1";

        public int BaudRate { get; set; } = 115200;
    }
}
