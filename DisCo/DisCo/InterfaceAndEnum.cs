using System;

namespace DistantConsole
{
    [Flags]
    public enum EDisCoDestinations
    {
        LogFile = 1,
        Distant = 2,
        Console = 4,
        EventHandler = 8
    }

    public enum EDisCoColors
    {
        Default,
        Normal,
        Inverted,
        Exception,
        Valid,
        Invalid,
        Unimportant,
        VeryImportant,
        Command,
        MiscColor0
    }

    public interface IDisCo
    {
        bool WriteLine(string aFormat, params object[] aParas);
        bool WriteLine(Exception aLine);
        bool WriteLine(object aLine);

        event Action<string> ConvertedMessageHandler;
        event Action<Exception> ExceptionMessageHandler;
        event Action<string, object[]> FormatMessageHandler;
        event Action<object> ObjectMessageHandler;

        event Action<string> MessageFromDistantHandler;
        event Action<string, EDisCoColors> MessageWithColorFromDistantHandler;

        IDisCo LimitDestinationsTo(EDisCoDestinations aWhiteList);
        IDisCo Color(EDisCoColors aColor);
        IDisCo Color(int aMiscColor);

        bool LogFileEnabled { get; set; }
        bool DistantEnabled { get; set; }
        bool ConsoleEnabled { get; set; }
        bool EventHandlerEnabled { get; set; }
    }

    public enum EDisCoConfigDisCoType
    {
        UDP,
        UDPBroadcast,
        TCPClient,
        TCPServer
    }
    public interface IDisCoConfig
    {
        EDisCoConfigDisCoType GetDisCoType();
        string GetTargetIP();
        int GetTargetPort();
        string GetLogPath();
    }

}