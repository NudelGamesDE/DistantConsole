using System;

namespace DistantConsole
{
    public abstract class DisCoBase : IDisCo
    {
        public const string Version = "0.0.4";

        public DisCoBase()
        {
            ConsoleEnabled = true;
            ConsoleEnabledOnce = true;
            LogFileEnabled = true;
            LogFileEnabledOnce = true;
            EventHandlerEnabled = true;
            EventHandlerEnabledOnce = true;
            DistantEnabled = true;
            DistantEnabledOnce = true;
        }

        public bool WriteLine(string aFormat, params object[] aParas)
        {
            var _message = String.Format(aFormat, aParas);
            WriteConsole(_message);
            WriteLogFile(_message);
            if (EventHandlerEnabled && EventHandlerEnabledOnce)
            {
                try
                {
                    if (ConvertedMessageHandler != null) ConvertedMessageHandler(_message);
                    if (FormatMessageHandler != null) FormatMessageHandler(aFormat, aParas);
                }
                catch (Exception) { }
            }
            var ret = true;
            if (DistantEnabled && DistantEnabledOnce)
                WriteLineDistant(aFormat, aParas);
            AfterWrite();
            return ret;
        }

        public bool WriteLine(Exception aLine)
        {
            if (NextColor == EDisCoColors.Default) NextColor = EDisCoColors.Exception;
            var _message = aLine.ToString();
            WriteConsole(_message);
            WriteLogFile(_message);
            if (EventHandlerEnabled && EventHandlerEnabledOnce)
            {
                try
                {
                    if (ConvertedMessageHandler != null) ConvertedMessageHandler(_message);
                    if (ExceptionMessageHandler != null) ExceptionMessageHandler(aLine);
                }
                catch (Exception) { }
            }
            var ret = true;
            if (DistantEnabled && DistantEnabledOnce)
                WriteLineDistant(aLine);
            AfterWrite();
            return ret;
        }

        public bool WriteLine(object aLine)
        {
            var _message = aLine.ToString();
            WriteConsole(_message);
            WriteLogFile(_message);
            if (EventHandlerEnabled && EventHandlerEnabledOnce)
            {
                try
                {
                    if (ConvertedMessageHandler != null) ConvertedMessageHandler(_message);
                    if (ObjectMessageHandler != null) ObjectMessageHandler(aLine);
                }
                catch (Exception) { }
            }
            var ret = true;
            if (DistantEnabled && DistantEnabledOnce)
                WriteLineDistant(aLine);
            AfterWrite();
            return ret;
        }

        private void AfterWrite()
        {
            LogFileEnabledOnce = true;
            DistantEnabledOnce = true;
            ConsoleEnabledOnce = true;
            EventHandlerEnabledOnce = true;
            NextColor = EDisCoColors.Default;
        }

        protected virtual bool WriteLineDistant(string aFormat, params object[] aParas)
        {
            return WriteLineDistantBase(String.Format(aFormat, aParas));
        }
        protected virtual bool WriteLineDistant(Exception aLine)
        {
            return WriteLineDistantBase(aLine.ToString());
        }
        protected virtual bool WriteLineDistant(object aLine)
        {
            return WriteLineDistantBase(aLine.ToString());
        }

        protected abstract bool WriteLineDistantBase(string aMessage);

        public event Action<string> ConvertedMessageHandler;
        public event Action<Exception> ExceptionMessageHandler;
        public event Action<string, object[]> FormatMessageHandler;
        public event Action<object> ObjectMessageHandler;
        public event Action<string> MessageFromDistantHandler;

        protected void GotDistantMessage(string aMessage)
        {
            var _handler = MessageFromDistantHandler;
            if (_handler != null) _handler(aMessage);
        }

        public IDisCo LimitDestinationsTo(EDisCoDestinations aWhiteList)
        {
            LogFileEnabledOnce = (aWhiteList & EDisCoDestinations.LogFile) == EDisCoDestinations.LogFile;
            DistantEnabledOnce = (aWhiteList & EDisCoDestinations.Distant) == EDisCoDestinations.Distant;
            ConsoleEnabledOnce = (aWhiteList & EDisCoDestinations.Console) == EDisCoDestinations.Console;
            EventHandlerEnabledOnce = (aWhiteList & EDisCoDestinations.EventHandler) == EDisCoDestinations.EventHandler;
            return this;
        }

        public IDisCo Color(EDisCoColors aColor)
        {
            NextColor = aColor;
            return this;
        }

        public IDisCo Color(int aMiscColor)
        {
            NextColor = EDisCoColors.MiscColor0 + aMiscColor;
            return this;
        }

        public virtual bool LogFileEnabled { get; set; }
        public virtual bool DistantEnabled { get; set; }
        public virtual bool ConsoleEnabled { get; set; }
        public virtual bool EventHandlerEnabled { get; set; }

        [ThreadStatic]
        private static bool LogFileEnabledOnce;
        [ThreadStatic]
        private static bool DistantEnabledOnce;
        [ThreadStatic]
        private static bool ConsoleEnabledOnce;
        [ThreadStatic]
        private static bool EventHandlerEnabledOnce;
        [ThreadStatic]
        protected static EDisCoColors NextColor;

        public bool LogFileTimeStamp { get; set; }

        protected string LogPath = "log.txt";
        private bool NotFirstLogWrite;
        private void WriteLogFile(string aMessage)
        {
            try
            {
                if (!LogFileEnabled || !LogFileEnabledOnce) return;
                using (var file = new System.IO.StreamWriter(LogPath, NotFirstLogWrite))
                {
                    file.WriteLine(LogFileTimeStamp ? MessageTimeStampConverter(aMessage) : aMessage);
                }
                NotFirstLogWrite = true;
            }
            catch (Exception) { }
        }

        public static string getTimeStamp()
        {
            return string.Format("[{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00},{6:0000}]",
                                DateTime.Now.Year,
                                DateTime.Now.Month,
                                DateTime.Now.Day,
                                DateTime.Now.Hour,
                                DateTime.Now.Minute,
                                DateTime.Now.Second,
                                DateTime.Now.Millisecond);
        }
        public static string MessageTimeStampConverter(string aMessage)
        {
            return getTimeStamp() + "\t" + aMessage.Replace("\n", "\n\t\t\t\t");
        }


        private void WriteConsole(string aMessage)
        {
            if (!ConsoleEnabled || !ConsoleEnabledOnce) return;
            Console.WriteLine(aMessage);
        }

    }
}
