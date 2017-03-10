using DistantConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisCoTester
{
    class Program
    {
        static void Main(string[] args)
        {
            DisCo.UseInterface(new DisCoUDPBroadcast("test.txt", 1850));

            DisCo.WriteLine("test from tester {0}", 1);

            Thread.Sleep(2500);
        }
    }
}
