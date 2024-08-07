
using System.Net;
using System.Net.Sockets;

TcpClientTicTacToe tictactoe = new("127.0.0.1", 6000);
tictactoe.Start();
tictactoe.@event.WaitOne();

public class TcpClientTicTacToe
{
    IPAddress _iP;
    int _port;
    public ManualResetEvent @event = new(false);
    TcpClient Client { get; set; }
    NetworkStream NetworkStream { get; set; }
    public TcpClientTicTacToe(string ipAdress, int port)
    {
        Client = new TcpClient(ipAdress, port);
        _iP = IPAddress.Parse(ipAdress);
        _port = port;
    }
    public void Start()
    {
        NetworkStream = Client.GetStream();
        StreamReader streamReader = new StreamReader(NetworkStream);
        StreamWriter streamWriter = new StreamWriter(NetworkStream)
        {
            AutoFlush = true
        };

        while (true)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Console.WriteLine($"Message Fromm: {streamReader.ReadLine()}");
                }
            });

            //Console.WriteLine(streamReader.ReadLine()); 
            streamWriter.WriteLine(Console.ReadLine());
        }

    }
}
//TelegrammClient telegrammClient = new("127.0.0.1", 6000);
//telegrammClient.ClientName = "Shahla";
//telegrammClient.Start();
//telegrammClient.Send("pups");
//telegrammClient.M += action;
//foreach (var message in telegrammClient.Messages)
//{
//    Console.WriteLine(message);
//}
//telegrammClient.Wait();


//void action()
//{ Console.WriteLine("I am typing from action"); };

//public interface ChatClieint
//{
//    IPAddress IpAddress { get; set; }
//    IPEndPoint IpEndPoint { get; set; }
//    Socket ClientSocket { get; set; }
//    string ClientName { get; set; }
//    ManualResetEvent manualResetEvent { get; set; }
//    List<string> Messages { get; set; }
//    public Task Send(string message);
//    public void Start();

//}
//public class TelegrammClient : ChatClieint
//{
//    public event Action M; 
//    public IPAddress IpAddress { get; set; }
//    public IPEndPoint IpEndPoint { get; set; }
//    public Socket ClientSocket { get; set; }
//    public string? ClientName { get; set; }
//    public ManualResetEvent manualResetEvent { get; set; }
//    public List<string> Messages { get; set; }
//    private byte[] data;
//    public StringBuilder stringBuilder;
//    public TelegrammClient(string ipAdress, int portNum)
//    {
//        manualResetEvent = new ManualResetEvent(false);
//        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
//        IpAddress = IPAddress.Parse(ipAdress);
//        IpEndPoint = new IPEndPoint(IpAddress, portNum);
//        Messages = new();
//    }
//    public Task Send(string message)
//    {
//        return Task.Factory.StartNew(() =>
//        {
//            try
//            {
//                Console.Write("Write a message: ");
//                ClientSocket.Send(Encoding.UTF8.GetBytes($"{ClientName}: {message}"));
//                M?.Invoke();
//            }
//            catch (SocketException)
//            {
//                return;
//            }
//        }, TaskCreationOptions.LongRunning);

//    }

//    public void Start()
//    {
//        if (ClientName is null or "")
//        {
//            return;
//        }
//        try
//        {
//            Console.WriteLine("Connecting to server...");
//            ClientSocket.Connect(IpEndPoint);
//            Console.WriteLine("Connected!");
//        }
//        catch (SocketException)
//        {
//            return;
//        }
//    }
//    public void Recive()
//    {
//        data = new byte[256]; // буфер для ответа
//        stringBuilder = new StringBuilder();
//        do
//        {
//            ClientSocket.Receive(data);
//            stringBuilder.Append(Encoding.UTF8.GetString(data));
//        }
//        while (ClientSocket.Available > 0);
//        Messages.Add(stringBuilder.ToString());

//    }
//    public void Wait()
//    {
//        manualResetEvent.WaitOne();
//    }
//}



//ManualResetEvent manualResetEvent = new ManualResetEvent(false);

//Task.Factory.StartNew(() =>
//{
//    using Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
//    IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
//    IPEndPoint endPoint = new IPEndPoint(ipAddress, 6000);
//    Console.WriteLine("Enter your nickname: ");
//    string? clientName = Console.ReadLine();

//    if (clientName is null or "")
//    {
//        return;
//    }
//    try
//    {
//        Console.WriteLine("Connecting to server...");
//        client.Connect(endPoint);
//        Console.WriteLine("Connected!");
//    }
//    catch (SocketException)
//    {
//        return;
//    }
//    while (true)
//    {
//        Console.Write("Write a message: ");
//        string? message = Console.ReadLine();

//        if (message is null or "exit")
//        {
//            return;
//        }
//        try
//        {
//            client.Send(Encoding.UTF8.GetBytes($"{clientName}: {message}"));
//        }
//        catch (SocketException)
//        {
//            return;
//        }
//    }
//}, TaskCreationOptions.LongRunning);
//manualResetEvent.WaitOne();









































