using System;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using NodaTime;

namespace RemoteControlV3.Logging
{
    public class Logger : IDisposable
    {
        public static LogLevel ConsoleVerbosity = LogLevel.Info;

        private Thread logThread;//the thread looping and saving logs from the queue

        private ConcurrentQueue<LogMessage> logQueue;//the thread-safe queue

        private bool logging = true;//set to false to stop logging

        public bool IsLogging => logThread.ThreadState == ThreadState.Running;

        private StreamWriter log;

        public Logger()
        {
            logQueue = new ConcurrentQueue<LogMessage>();

            log = File.AppendText("log.log");

            logThread = new Thread(new ThreadStart(LogLoop));
            logThread.Name = "LoggerThread";
            logThread.IsBackground = true;
            logThread.Start();
        }

        public void Log(LogType type, LogLevel severity, string message)
        {
            LogMessage m = new LogMessage(message, type, severity);
            logQueue.Enqueue(m);
        }

        public void Log(LogMessage message)
        {
            logQueue.Enqueue(message);
        }

        public void Log(string message)
        {
            LogMessage m = new LogMessage(message);
            logQueue.Enqueue(m);
        }

        public void Dispose()
        {
            logging = false;
            log.Flush();
            log.Close();
        }


        public void Flush()
        {
            log.Flush();
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
                Console.WriteLine(message.ToString());
            }
        }

        private void DiskLog(LogMessage message)
        {
            log.Write(message.ToString());
        }
    }
}