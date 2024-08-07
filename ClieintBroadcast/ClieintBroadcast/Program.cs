using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

 const string host = "127.0.0.1";
User _clientData = new User();
const int port = 44433;
NetworkStream _stream;
TcpClient _client;
do
{
    Console.Write("\t Enter your name: ");
    _clientData.Name = Console.ReadLine();
    _client = new TcpClient();

} while (_clientData.Name is null or "");

try
{
    _client.Connect(host, port);
    _stream = _client.GetStream();

    string message = _clientData.Name;
    byte[] buffer = Encoding.Unicode.GetBytes(message);
    _stream.Write(buffer, 0, buffer.Length);

    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
    receiveThread.Start(); //старт потока
    Console.WriteLine("\t\t Welcome, {0}", _clientData.Name);
    SendMessage();
}
catch (Exception e)
{
    Console.WriteLine(String.Format("\t Error accepted! {0}", e.Message));
}
finally
{
    //Disconnect();
}

// отправка сообщений
void SendMessage()
{
    Console.WriteLine("\t\t Enter the message: ");

    while (true)
    {
        string message = Console.ReadLine();
        byte[] buffer = Encoding.Unicode.GetBytes(message);
        _stream.Write(buffer, 0, buffer.Length);
    }
}
void ReceiveMessage()
{
    while (true)
    {
        try
        {
            byte[] buffer = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = _stream.Read(buffer, 0, buffer.Length);
                builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            }
            while (_stream.DataAvailable);

            string message = builder.ToString();
            Console.WriteLine(message);
        }
        catch
        {
            Console.WriteLine("\t\t Connection interrupted!");
            Console.ReadLine();
            Disconnect();
        }
    }
}

void Disconnect()
{
    
    if (_stream != null)
        _stream.Close();
    if (_client != null)
        _client.Close();
    Environment.Exit(0);
}

class User
{
    public string Id { get; set; }
    public string Name { get; set; }
}
