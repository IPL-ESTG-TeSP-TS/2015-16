using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace W7_Client
{
    class Client
    {
        private const int PORT = 10000;

        static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, PORT);
            TcpClient client = new TcpClient();

            Console.WriteLine("READY? press any key");
            Console.ReadKey();

            client.Connect(endpoint);

            Console.WriteLine("READY? press any key");
            Console.ReadKey();

            NetworkStream networkStream = client.GetStream();
            ProtocolSI protocolSI = new ProtocolSI();

            String msg = "Hello World";
            Console.WriteLine("Sending {0} to Server", msg);
            byte[] packet = protocolSI.Make(ProtocolSICmdType.DATA, msg);
            networkStream.Write(packet, 0, packet.Length);

            Console.WriteLine("Waiting for ACK from Server");
            networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Sending {0} to Server", i);
                packet = protocolSI.Make(ProtocolSICmdType.DATA, i);
                networkStream.Write(packet, 0, packet.Length);

                Console.WriteLine("Waiting for ACK from Server");
                networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
            }

            Console.WriteLine("Sending EOT to Server");
            packet = protocolSI.Make(ProtocolSICmdType.EOT);
            networkStream.Write(packet, 0, packet.Length);

            Console.WriteLine("Waiting for ACK from Server");
            networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);

            networkStream.Close();
            client.Close();
        }
    }
}
