using System;
using System.IO.Ports;

namespace RemoteControlV3.Connection
{
    public class SerialConnection : IDisposable
    {
        private SerialPort Port;

        public SerialConnection(ConnectionInfo info)
        {
            Port = new SerialPort(info.Port, info.BaudRate, Parity.None, 8, StopBits.One);
            Port.DataReceived += (sender, args) =>
            {
                Program.CommandHandler.Parse(Port.ReadExisting());
            };
            Port.Open();
        }

        public void Dispose()
        {
            Port.Dispose();
        }
    }
}