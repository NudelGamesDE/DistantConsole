using System;

namespace DistantConsole
{
    public static class DisCo
    {
        private static float test;
        private class GlobalDisCo : IDisCo
        {
            public bool WriteLine(string aFormat, params object[] aParas) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.WriteLine(aFormat, aParas); return false; }
            public bool WriteLine(Exception aLine) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.WriteLine(aLine); return false; }
            public bool WriteLine(object aLine) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.WriteLine(aLine); return false; }

            public event Action<string> ConvertedMessageHandler;
            public void ConvertedMessage(string aMessage) { var _handler = ConvertedMessageHandler; if (_handler != null) _handler(aMessage); }
            public event Action<Exception> ExceptionMessageHandler;
            public void ExceptionMessage(Exception aException) { var _handler = ExceptionMessageHandler; if (_handler != null) _handler(aException); }
            public event Action<string, object[]> FormatMessageHandler;
            public void FormatMessage(string aFormat, object[] aParas) { var _handler = FormatMessageHandler; if (_handler != null) _handler(aFormat, aParas); }
            public event Action<object> ObjectMessageHandler;
            public void ObjectMessage(object aMessage) { var _handler = ObjectMessageHandler; if (_handler != null) _handler(aMessage); }

            public event Action<string> MessageFromDistantHandler;
            public void MessageFromDistant(string aMessage) { var _handler = MessageFromDistantHandler; if (_handler != null) _handler(aMessage); }

            public IDisCo LimitDestinationsTo(EDisCoDestinations aWhiteList) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.LimitDestinationsTo(aWhiteList); return this; }
            public IDisCo Color(EDisCoColors aColor) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.Color(aColor); return this; }
            public IDisCo Color(int aMiscColor) { var _temp = RealGlobalDisco; if (_temp != null) return _temp.Color(aMiscColor); return this; }

            public bool LogFileEnabled { get { var _temp = RealGlobalDisco; if (_temp != null) return _temp.LogFileEnabled; return false; } set { var _temp = RealGlobalDisco; if (_temp != null) _temp.LogFileEnabled = value; } }
            public bool DistantEnabled { get { var _temp = RealGlobalDisco; if (_temp != null) return _temp.DistantEnabled; return false; } set { var _temp = RealGlobalDisco; if (_temp != null) _temp.DistantEnabled = value; } }
            public bool ConsoleEnabled { get { var _temp = RealGlobalDisco; if (_temp != null) return _temp.ConsoleEnabled; return false; } set { var _temp = RealGlobalDisco; if (_temp != null) _temp.ConsoleEnabled = value; } }
            public bool EventHandlerEnabled { get { var _temp = RealGlobalDisco; if (_temp != null) return _temp.EventHandlerEnabled; return false; } set { var _temp = RealGlobalDisco; if (_temp != null) _temp.EventHandlerEnabled = value; } }
        }
        private static readonly GlobalDisCo GlobalDisCo_ = new GlobalDisCo();
        public static IDisCo GetGlobal() { return GlobalDisCo_; }
        private static IDisCo RealGlobalDisco;

        public static bool WriteLine(string aFormat, params object[] aParas) { return GlobalDisCo_.WriteLine(aFormat, aParas); }
        public static bool WriteLine(Exception aLine) { return GlobalDisCo_.WriteLine(aLine); }
        public static bool WriteLine(object aLine) { return GlobalDisCo_.WriteLine(aLine); }

        public static event Action<string> ConvertedMessageHandler { add { GlobalDisCo_.ConvertedMessageHandler += value; } remove { GlobalDisCo_.ConvertedMessageHandler -= value; } }
        public static event Action<Exception> ExceptionMessageHandler { add { GlobalDisCo_.ExceptionMessageHandler += value; } remove { GlobalDisCo_.ExceptionMessageHandler -= value; } }
        public static event Action<string, object[]> FormatMessageHandler { add { GlobalDisCo_.FormatMessageHandler += value; } remove { GlobalDisCo_.FormatMessageHandler -= value; } }
        public static event Action<object> ObjectMessageHandler { add { GlobalDisCo_.ObjectMessageHandler += value; } remove { GlobalDisCo_.ObjectMessageHandler -= value; } }

        public static event Action<string> MessageFromDistantHandler { add { GlobalDisCo_.MessageFromDistantHandler += value; } remove { GlobalDisCo_.MessageFromDistantHandler -= value; } }

        public static IDisCo LimitDestinationsTo(EDisCoDestinations aWhiteList) { return GlobalDisCo_.LimitDestinationsTo(aWhiteList); }
        public static IDisCo Color(EDisCoColors aColor) { return GlobalDisCo_.Color(aColor); }
        public static IDisCo Color(int aMiscColor) { return GlobalDisCo_.Color(aMiscColor); }

        public static bool LogFileEnabled { get { return GlobalDisCo_.LogFileEnabled; } set { GlobalDisCo_.LogFileEnabled = value; } }
        public static bool DistantEnabled { get { return GlobalDisCo_.DistantEnabled; } set { GlobalDisCo_.DistantEnabled = value; } }
        public static bool ConsoleEnabled { get { return GlobalDisCo_.ConsoleEnabled; } set { GlobalDisCo_.ConsoleEnabled = value; } }
        public static bool EventHandlerEnabled { get { return GlobalDisCo_.EventHandlerEnabled; } set { GlobalDisCo_.EventHandlerEnabled = value; } }

        public static void LoadFromConfig(IDisCoConfig aConfig)
        {
            switch (aConfig.GetDisCoType())
            {
                case EDisCoConfigDisCoType.TCPClient: break;
                case EDisCoConfigDisCoType.TCPServer: break;
                case EDisCoConfigDisCoType.UDP: break;
                case EDisCoConfigDisCoType.UDPBroadcast:
                    UseInterface(new DisCoUDPBroadcast(aConfig.GetLogPath(), aConfig.GetTargetPort()));
                    break;
            }
        }

        public static void UseInterface(IDisCo aDisCo)
        {
            aDisCo.ConvertedMessageHandler += GlobalDisCo_.ConvertedMessage;
            aDisCo.ExceptionMessageHandler += GlobalDisCo_.ExceptionMessage;
            aDisCo.FormatMessageHandler += GlobalDisCo_.FormatMessage;
            aDisCo.ObjectMessageHandler += GlobalDisCo_.ObjectMessage;
            aDisCo.MessageFromDistantHandler += GlobalDisCo_.MessageFromDistant;
            RealGlobalDisco = aDisCo;
        }

        public static string Version { get { return DisCoBase.Version; } }
    }
}
