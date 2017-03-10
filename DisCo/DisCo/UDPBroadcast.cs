using SimpleTCP;
using System.Linq;

namespace DistantConsole
{
    public class DisCoUDPBroadcast : DisCoBase
    {
        private SimpleUDP Broadcaster;

        public DisCoUDPBroadcast(string aLogPath, int aPort)
        {
            LogPath = aLogPath;
            Broadcaster = new SimpleUDP("255.255.255.255", aPort);
        }


        protected override bool WriteLineDistantBase(string aMessage)
        {
            return Broadcaster.Send(aMessage.Select(v => (byte)v).ToArray());
        }
    }
}