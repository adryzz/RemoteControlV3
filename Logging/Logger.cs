/*using System;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;

namespace RemoteControlV3.Logging
{
    public class Logger : IDisposable
    {
        public LogLevel ConsoleVerbosity = LogLevel.Info;

        private Thread logThread;//the thread looping and saving logs from the queue

        private ConcurrentQueue<LogMessage> logQueue;//the thread-safe queue

        private bool logging = true;//set to false to stop logging

        public bool IsLogging => logThread.ThreadState == ThreadState.Running;

        private StreamWriter runtimeLog;

        private StreamWriter networkLog;

        private StreamWriter commandsLog;

        public void Initialize()
        {
            logQueue = new ConcurrentQueue<LogMessage>();

            Directory.CreateDirectory("Logs");

            runtimeLog = File.AppendText(Path.Combine("Logs", "runtime.log"));
            networkLog = File.AppendText(Path.Combine("Logs", "network.log"));
            commandsLog = File.AppendText(Path.Combine("Logs", "commands.log"));

            logThread = new Thread(new ThreadStart(LogLoop));
            logThread.Name = "LoggerThread";
            logThread.IsBackground = true;
            logThread.Start();
        }

        public void Log(LogType type, LogLevel severity, string message)
        {
            LogMessage m = new LogMessage()
            {
                LogTime = DateTime.Now,
                Type = type,
                Severity = severity,
                Message = message
            };
            logQueue.Enqueue(m);
        }

        public void Log(LogMessage message)
        {
            logQueue.Enqueue(message);
        }

        public void Dispose()
        {
            logging = false;
            runtimeLog.Flush();
            runtimeLog.Close();
            networkLog.Flush();
            networkLog.Close();
            commandsLog.Flush();
            commandsLog.Close();
        }


        public void Flush()
        {
            runtimeLog.Flush();
            networkLog.Flush();
            commandsLog.Flush();
        }

        private void LogLoop()
        {
            while (logging)
            {
                //dequeue logs and save them to disk
                while (!logQueue.IsEmpty)
                {
                    LogMessage message;
                    if (!logQueue.TryDequeue(out message))
                    {
                        Thread.Sleep(50);//if queue is busy, wait 50ms
                    }
                    ConsoleLog(message);
                    DiskLog(message);
                }
                Thread.Sleep(100);
            }
        }

        private void ConsoleLog(LogMessage message)
        {
            if ((int)ConsoleVerbosity <= (int)message.Severity)//log to console only if verbosity is lower or equal
            {
                Console.Write("[");
                Console.ForegroundColor = GetColor(message.Severity);
                Console.Write(message.Severity.ToString().ToUpper());
                Console.ResetColor();
                Console.Write($"] | [");
                Console.ForegroundColor = GetColor(message.Type);
                Console.Write(message.Type.ToString().ToUpper());
                Console.ResetColor();
                Console.WriteLine($"] {message.LogTime} | {message.Message.Replace("\n", "").Replace("\r", "")}");
            }
        }

        private void DiskLog(LogMessage message)
        {
            string log = $"[{message.Severity.ToString().ToUpper()}] | {message.LogTime} | {message.Message.Replace("\n", "").Replace("\r", "")}\n";
            switch (message.Type)
            {
                case LogType.Runtime:
                    {
                        runtimeLog.Write(log);
                        break;
                    }
                case LogType.Network:
                    {
                        networkLog.Write(log);
                        break;
                    }
                case LogType.Commands:
                    {
                        commandsLog.Write(log);
                        break;
                    }
                case LogType.Database:
                    {
                        databaseLog.Write(log);
                        break;
                    }
            }
        }
    }
}*/