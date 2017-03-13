using SimpleTCP;
using System.Linq;

namespace DistantConsole
{
    public class DisCoClient: DisCoBase
    {
        private SimpleClient Client;

        public DisCoClient(string aLogPath, string aIp, int aPort)
        {
            Client = new SimpleClient(aIp, aPort);
            Client.ReceiveAction += aBytes =>
            {
                GotDistantMessage(new string(aBytes.Select(v => (char)v).ToArray()));
            };
        }

        protected override bool WriteLineDistantBase(string aMessage)
        {
            return Client.Send((DisCo.EDisCoColorToChar(NextColor) + aMessage).Select(v => (byte)v).ToArray());
        }
    }
}
