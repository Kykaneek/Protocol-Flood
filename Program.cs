using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        //Ustawienie portu na sztywno
        const int PORT = 5000;
        //Przypisanie klucza XOR do deszyfracji
        const byte XOR_KEY = 0xAA;

        static void Main()
        {
            //Nasłuchiwanie wszystkich adresów IP na wskazany port
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();

            Console.WriteLine("Port " + PORT);

            while (true)
            {
                //Oczekiwanie na połączenie klienta
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                try
                {
                    //Odczytanie ramek do listy
                    List<string> receivedMessages = new List<string>();
                    byte[] buffer = new byte[1024];

                    //Odczytanie danuch z wiadmości klienta
                    while (stream.DataAvailable || receivedMessages.Count == 0)
                    {
                        //Wczytanie do bufora
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                            break;

                        //Index startowy do parsowania ramek
                        int offset = 0;
                        while (offset + 12 <= bytesRead) // 12 bajtów nagłówek
                        {
                            short magic = BitConverter.ToInt16(buffer, offset);      // magic number (0xCAFE)
                            byte version = buffer[offset + 2];                       // wersja protokołu
                            byte type = buffer[offset + 3];                          // typ ramki (np. 1 = dane)
                            int length = BitConverter.ToInt32(buffer, offset + 4);   // długość payload
                            int checksum = BitConverter.ToInt32(buffer, offset + 8); // suma kontrolna fragmentu

                            // Weryfikacja ramki w buforze - oczekiwanie na reszte
                            if (offset + 12 + length > bytesRead)
                            {
                                break;
                            }
                            //Pobranie do osobnej tablicy
                            byte[] payload = new byte[length];
                            Array.Copy(buffer, offset + 12, payload, 0, length);
                            //Odszyfrowanie
                            Xor(payload);

                            //Weryfikacja sumy kontrolnej
                            if (Checksum(payload) == checksum)
                            {
                                string message = Encoding.UTF8.GetString(payload);  //Dekodowanie nagłówka
                                Console.WriteLine($"[OK] Received: {message}");     //Odczytanie / zalogowanie wiadomości przez serwer
                                receivedMessages.Add(message);                      //Zwrócenie wiadomości do klienta
                            }
                            else
                            {
                                Console.WriteLine("[ERROR] Checksum invalid");      //Zwrócenie komunikatu o błędnej sumie kontrolnej
                            }
                            //Przesunięcie offsetu o długość całej ramki
                            offset += 12 + length;
                        }
                    }

                    //Zwrócenie całej wiadomości do klienta - odpowiedź serera
                    string combinedResponse = "Wiadomość dotarła z sukcesem - powrót wiadomości\n";
                    combinedResponse += string.Join(Environment.NewLine, receivedMessages);

                    byte[] respData = Encoding.UTF8.GetBytes(combinedResponse);
                    Xor(respData);

                    stream.Write(respData, 0, respData.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
                finally
                {
                    client.Close();
                }
            }
        }

        //Deszfracja za pomocą klucza Xor
        static void Xor(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] ^= XOR_KEY;
        }

        //Weryfikacja sumy kontrolnej
        static int Checksum(byte[] data)
        {
            int sum = 0;
            foreach (byte b in data)
                sum ^= b;
            return sum;
        }
    }
}
