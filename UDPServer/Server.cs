using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPServer
{
    class Server
    {
        private const int MAX_CHILDS = 5;
        private static BlockingCollection<int> pendingMessages;

        public static void Run()
        {
            pendingMessages = new BlockingCollection<int>(new ConcurrentQueue<int>());

            for(int i = 0; i < MAX_CHILDS; i++)
            {
                Thread childThread1 = new Thread(new ThreadStart(ProcessData));
                childThread1.Name = string.Format("childThread{0}", i);
                childThread1.Start();
            }


            //byte[] data = new byte[1024];
            //IPEndPoint udpSockAddress = new IPEndPoint(IPAddress.Any, 8080);
            //UdpClient udpServerSock = new UdpClient(udpSockAddress);

            Console.WriteLine("Waiting for a client...");

            //IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            while (1 == 1)
            {
                string line = System.Console.ReadLine();
                if (int.TryParse(line, out int number))
                {
                    Console.WriteLine(string.Format("MainThread: {0}", number));
                    pendingMessages.Add(number);
                }
                //data = udpServerSock.Receive(ref sender);
                //ProcessData(data);
                //Console.WriteLine("Message received from {0}:", sender.ToString());
                //Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            }

            //OLD STUFF HOW TO SEND
            //Console.WriteLine("Message received from {0}:", sender.ToString());
            //Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));

            //string welcome = "Welcome to my test server";
            //data = Encoding.ASCII.GetBytes(welcome);
            //udpSock.Send(data, data.Length, sender);

            //while (true)
            //{
            //    data = udpSock.Receive(ref sender);

            //    Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            //    udpSock.Send(data, data.Length, sender);
            //}
        }

        public static void ProcessData()
        {
            while (1 == 1)
            {
                int number = pendingMessages.Take();
                Console.WriteLine(string.Format("{1}: {0}", number, Thread.CurrentThread.Name));
            }
        }
    }
}
