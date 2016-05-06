using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1_Server
{
    class Server
    {
        static void Main(string[] args)
        {
            IPEndPoint endPoint;
            
            TcpListener tcpListener = null;
            TcpClient client = null;
            NetworkStream stream = null;

            try
            {
                
                endPoint = new IPEndPoint(IPAddress.Any, 9999);
                tcpListener = new TcpListener(endPoint);
                tcpListener.Start();
                client = tcpListener.AcceptTcpClient();
                stream = client.GetStream();

                // receber inteiro
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                Array.Resize(ref buffer, bytesRead);
                Console.WriteLine("recebido: " + );
                // enviar ack
                byte[] packet = protocol.Make(ProtocolSICmdType.ACK);
                stream.Write(packet, 0, packet.Length);
                Console.WriteLine("ack enviado");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
                if (client != null)
                    client.Close();
                if (tcpListener != null)
                    tcpListener.Stop();
            }
        }
    }
}
