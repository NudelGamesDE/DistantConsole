namespace DistantConsole
{
    public class DisCoUDPBroadcast : DisCoUDP
    {
        public DisCoUDPBroadcast(string aLogPath, int aPort) : base(aLogPath, "255.255.255.255", aPort)
        { }
    }
}