using SimpleTCP;
using System;
using System.Linq;

namespace DistantConsole
{
    public class DisCoUDP : DisCoBase
    {
        private SimpleUDP Sender;
        private SimpleUDP Receiver;

        public DisCoUDP(string aLogPath, string aIp, int aPort)
        {
            LogPath = aLogPath;
            Sender = new SimpleUDP(aIp, aPort);
            try
            {
                Receiver = new SimpleUDP(aPort);
                Receiver.ReceiveAction += aBytes =>
                {
                    GotDistantMessage(new string(aBytes.Select(v => (char)v).ToArray()));
                };
                CanReceive = true;
            }
            catch (Exception)
            {
                CanReceive = false;
            }
        }

        public void StopReceiving()
        {
            var _receiver = Receiver;
            Receiver = null;
            if (_receiver == null) return;

            _receiver.Stop();
            CanReceive = false;
        }

        public bool CanReceive { get; private set; }

        protected override bool WriteLineDistantBase(string aMessage)
        {
            return Sender.Send((DisCo.EDisCoColorToChar(NextColor) + aMessage).Select(v => (byte)v).ToArray());
        }
    }
}
