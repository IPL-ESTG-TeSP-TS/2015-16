using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1._2.Client
{
    class Client
    {
        static void Main(string[] args)
        {
            ProtocolSI protocol = new ProtocolSI();

            byte[]packet = protocol.Make(ProtocolSICmdType.DATA, "MG");

            stream.Read(protocol.Buffer,0,protocol.Buffer.Length)

                protocol.GetCmdType()
        }
    }
}
