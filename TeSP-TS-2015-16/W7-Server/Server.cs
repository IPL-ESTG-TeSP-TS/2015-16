using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace W7_Server
{
    class Server
    {
        private const int PORT = 10000;

        static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, PORT);
            TcpListener listener = new TcpListener(endpoint);
            listener.Start();
            Console.WriteLine("SERVER READY");

            int clientCounter = 0;
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clientCounter++;
                Console.WriteLine("Client {0} connected", clientCounter);

                ClientHandler clientHandler = new ClientHandler(client, clientCounter);
                clientHandler.Handle();
            }
        }
    }

    class ClientHandler
    {
        private TcpClient client;
        private int clientID;

        public ClientHandler(TcpClient client, int clientID)
        {
            this.client = client;
            this.clientID = clientID;
        }

        public void Handle()
        {
            Thread thread = new Thread(threadHandler);
            thread.Start();
        }

        private void threadHandler()
        {
            NetworkStream networkStream = this.client.GetStream();
            ProtocolSI protocolSI = new ProtocolSI();

            networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
            Console.WriteLine("Received {0} from client {1}", protocolSI.GetStringFromData(), clientID);

            Console.WriteLine("Sending ACK to Client {0}", clientID);
            byte[] packet = protocolSI.Make(ProtocolSICmdType.ACK);
            networkStream.Write(packet, 0, packet.Length);

            do
            {
                networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                if(protocolSI.GetCmdType() != ProtocolSICmdType.EOT)
                {
                    Console.WriteLine("Received {0} from client {1}", protocolSI.GetIntFromData(), clientID);
                }
                else
                {
                    Console.WriteLine("Received EOT from Client {0}", clientID);
                }

                Console.WriteLine("Sending ACK to Client {0}", clientID);
                packet = protocolSI.Make(ProtocolSICmdType.ACK);
                networkStream.Write(packet, 0, packet.Length);


            } while (protocolSI.GetCmdType() != ProtocolSICmdType.EOT);

            Console.WriteLine("Ending Thread from Client {0}", clientID);
            networkStream.Close();
            client.Close();
        }
    }
}
