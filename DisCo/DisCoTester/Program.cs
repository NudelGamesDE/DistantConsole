using DistantConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisCoTester
{
    public class testConfig : IDisCoConfig
    {
        public EDisCoConfigDisCoType DisCoType;
        public EDisCoConfigDisCoType GetDisCoType()
        {
            return DisCoType;
        }

        public string GetLogPath()
        {
            return "log.txt";
        }

        public string IP;
        public string GetTargetIP()
        {
            return IP;
        }

        public int Port;
        public int GetTargetPort()
        {
            return Port;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Version {0}", DisCoBase.Version);
            Console.WriteLine("SimpleTCP Version {0}", DisCoBase.SimpleTCPVersion);
            Console.WriteLine("");

            Console.WriteLine("1: TCP Client");
            Console.WriteLine("2: TCP Server");
            Console.WriteLine("3: UDP");
            Console.WriteLine("4: UDP Broadcast");

            var _config = new testConfig();

            var _key = Console.ReadKey();
            var _rightKey = false;
            while (!_rightKey)
            {
                _rightKey = true;
                switch (_key.Key)
                {
                    case ConsoleKey.D1:
                        _config.DisCoType = EDisCoConfigDisCoType.TCPClient;
                        break;
                    case ConsoleKey.D2:
                        _config.DisCoType = EDisCoConfigDisCoType.TCPServer;
                        break;
                    case ConsoleKey.D3:
                        _config.DisCoType = EDisCoConfigDisCoType.UDP;
                        break;
                    case ConsoleKey.D4:
                        _config.DisCoType = EDisCoConfigDisCoType.UDPBroadcast;
                        break;

                    default:
                        _rightKey = false;
                        break;
                }
            }
            Console.WriteLine("");
            Console.Write("IP: ");
            _config.IP = Console.ReadLine();
            Console.Write("Port: ");
            _config.Port = int.Parse(Console.ReadLine());

            DisCo.LoadFromConfig(_config);
            DisCo.ConsoleEnabled = false;

            DisCo.MessageFromDistantHandler += (aMessage) =>
            {
                Console.WriteLine(aMessage);
            };
            //DisCo.MessageWithColorFromDistantHandler += (aMessage, aColor) =>
            //{
            //    Console.WriteLine(aColor.ToString() + " " + aMessage);
            //};

            while (true)
            {
                DisCo.WriteLine(Console.ReadLine());
            }

        }
    }
}
