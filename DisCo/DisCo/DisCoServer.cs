using SimpleTCP;
using System.Linq;

namespace DistantConsole
{
    class DisCoServer : DisCoBase
    {
        private SimpleServer Server;

        public DisCoServer(string aLogPath, int aPort)
        {
            Server = new SimpleServer(aPort);
            Server.ReceiveAction += aBytes =>
            {
                GotDistantMessage(new string(aBytes.Select(v => (char)v).ToArray()));
            };
        }

        protected override bool WriteLineDistantBase(string aMessage)
        {
            return Server.Send((DisCo.EDisCoColorToChar(NextColor) + aMessage).Select(v => (byte)v).ToArray());
        }
    }
}
