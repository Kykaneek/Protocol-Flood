using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client
{
    public partial class Klient : Form
    {
        //Dane do ustawienia połącznia
        IPAddress IPv4;
        int Port;

        //Szyfrowanie danych w ramce i określenie wielkości ramki
        public const byte XOR_KEY = 0xAA;
        public const int FRAME_SIZE = 16;

        public Klient()
        {
            InitializeComponent();
        }

        //Ustawienie połączenia
        private void SetOptions_Click(object sender, EventArgs e)
        {
            string ipText = IP.Text.Trim();
            string portText = PortP.Text.Trim();
            if (string.IsNullOrEmpty(ipText))
            {
                MessageBox.Show("Nie podano adresu IP!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(portText))
            {
                MessageBox.Show("Nie podano portu!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IPAddress.TryParse(ipText, out _))
            {
                MessageBox.Show("Niepoprawny format adresu IP!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(portText, out int port) || port < 1 || port > 65535)
            {
                MessageBox.Show("Port musi być liczbą z zakresu 1–65535!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IPv4 = IPAddress.Parse(ipText);
            Port = int.Parse(portText);

            MessageBox.Show("Ustawiono połączenie! \n \n Adres IP: " + ipText + "\n Port: " + portText, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Wyczyszczenie pól informacyjnych
        private void Clean_Click(object sender, EventArgs e)
        {
            ToServerMessage.Text = "";
            FromServerMessage.Text = "";
        }

        //Wysłanie wiadomości do serwera i jego odpowiedzi
        private void SendMessage_Click(object sender, EventArgs e)
        {
            //Walidacja adresu IP i portu
            if (IPv4 is null || PortP is null)
            {
                MessageBox.Show(
                    "Sparametryzuj połączenie! \n \n Adres IP: " + IPv4 + "\n Port: " + PortP,
                    "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
                return;
            }

            if (string.IsNullOrEmpty(ToServerMessage.Text))
            {
                MessageBox.Show(
                    "Nie podano wiadomości!",
                    "Operacja niedozwolona", MessageBoxButtons.OK, MessageBoxIcon.Stop
                );
                return;
            }

           
            try
            {
                FromServerMessage.Text = "";

                //ustawienie połączenia
                using TcpClient client = new TcpClient();
                client.Connect(IPv4, Port);

                //Pozyskanie strumienia
                using NetworkStream stream = client.GetStream();

                // Przygotowanie wiadomości
                string text = ToServerMessage.InvokeRequired
                    ? (string)ToServerMessage.Invoke(new Func<string>(() => ToServerMessage.Text))
                    : ToServerMessage.Text;

                //Zakodowanie informacji do UTF8
                byte[] data = Encoding.UTF8.GetBytes(text);
                
                //Podzielenie wiadomości na ramki
                var frames = Split(data, 16); 

                //Podzielenie ramki na kawałki
                foreach (var chunk in frames)
                {
                    //Obliczenie sumy kontrolnej
                    int checksum = Checksum(chunk);
                    //Szyfrowanie kluczem XOR
                    Xor(chunk);

                    //Podzielenie ramki na części
                    List<byte> frame = new List<byte>();
                    frame.AddRange(BitConverter.GetBytes((ushort)0xCAFE));  //Magic Number
                    frame.Add(1);                                           //wersja protokołu
                    frame.Add(1);                                           //typ ramki
                    frame.AddRange(BitConverter.GetBytes(chunk.Length));    //długość payload
                    frame.AddRange(BitConverter.GetBytes(checksum));        //suma kontrolna
                    frame.AddRange(chunk);                                  //zaszyfrowanie ramki

                    stream.Write(frame.ToArray(), 0, frame.Count);          //wysłanie ramki do serwera
                }

                //Odebranie odpowiedzi od serwera
                List<byte> allResponse = new List<byte>();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    allResponse.AddRange(buffer[..bytesRead]);

                    // jeśli w buforze nie ma więcej danych, przerwij
                    if (!stream.DataAvailable) break;
                }

                //Zdekodowanie całej odpowiedzi
                byte[] respData = allResponse.ToArray();
                Xor(respData);
                string resp = Encoding.UTF8.GetString(respData);

                //Odczytanie odpowiedzi
                FromServerMessage.Invoke(() =>
                {
                    FromServerMessage.AppendText("Server: " + resp + Environment.NewLine);
                });

                client.Close();
            }
            catch (Exception ex)
            {
                //Zwrócenie błędu w przypadku np. utraty połączenia z serwerem
                FromServerMessage.Invoke(() =>
                {
                    FromServerMessage.AppendText("ERROR: " + ex.Message + Environment.NewLine);
                });
            }
        }

        //Szyfrownaie z kluczem Xor do 
        static void Xor(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] ^= XOR_KEY;
        }
        //Obliczneie sumy kontrolnej
        static int Checksum(byte[] data)
        {
            int sum = 0;
            foreach (byte b in data)
                sum ^= b;
            return sum;
        }

        //Podzielenie wiadomości na fragmęty
        static List<byte[]> Split(byte[] data, int size)
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i < data.Length; i += size)
            {
                int len = Math.Min(size, data.Length - i);
                byte[] chunk = new byte[len];
                Array.Copy(data, i, chunk, 0, len);
                list.Add(chunk);
            }
            return list;
        }

    }
}
