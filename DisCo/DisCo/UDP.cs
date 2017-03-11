using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DistantConsole
{
    public class DisCoUDP : DisCoBase
    {
        private SimpleUDP Sender;

        public DisCoUDP(string aLogPath, string aIP, int aPort)
        {
            LogPath = aLogPath;
            Sender = new SimpleUDP(aIP, aPort);
            Sender.ReceiveAction = (aBytes) =>
            {
                GotDistantMessage(new string(aBytes.Select(v => (char)v).ToArray()));
            };
        }

        protected override bool WriteLineDistantBase(string aMessage)
        {
            return Sender.Send(aMessage.Select(v => (byte)v).ToArray());
        }
    }
}
