using System.Net;
using System.Net.Sockets;
using System.Text;

ChatServer _server = new();

try
{
     var _mainThread = new Thread(new ThreadStart(_server.Listen));
    _mainThread.Start();
}
catch (Exception e)
{
    _server.Disconnect();
    Console.WriteLine(String.Format("\t Error accepted! {0}", e.Message));
}
public class User 
{
    public string Id { get; set; } 
    public string Name { get; set; }    
}

public class ChatClient
{    public User User { get; private set; }
    public NetworkStream Stream { get; private set; }

    private TcpClient _client;
    private ChatServer _server;
    public ChatClient(TcpClient tcpClient, ChatServer serverObject)
    {
        User = new User();
        User.Id = Guid.NewGuid().ToString();
        _client = tcpClient;
        _server = serverObject;
        serverObject.AddClient(this);
    }

    public void Start()
    {
        try
        {
            Stream = _client.GetStream();
            string message = Read();
            User.Name = message;

            message = $"\t  {User.Name} entered the chat room!";
            _server.BroadcastMessage(message, this.User.Id);
            Console.WriteLine(message);

            while (true)
            {
                try
                {
                    message = Read();
                    message = String.Format(" {0}: {1}", User.Name, message);
                    Console.WriteLine(message);
                    _server.BroadcastMessage(message, this.User.Id);
                }
                catch
                {
                    message = String.Format(" {0}: left the chat room", User.Name);
                    Console.WriteLine(message);
                    _server.BroadcastMessage(message, this.User.Id);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(String.Format("\t Error accepted! {0}", e.Message) );
        }
        finally
        {
            _server.RemoveClient(this.User.Id);
            Close();
        }
    }

    private string Read()
    {
        byte[] buffer = new byte[64]; 
        StringBuilder builder = new StringBuilder();
        int bytes = 0;
        do
        {
            bytes = Stream.Read(buffer, 0, buffer.Length);
            builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
        }
        while (Stream.DataAvailable);

        return builder.ToString();
    }

    public void Close()
    {
        if (Stream != null)
            Stream.Close();
        if (_client != null)
            _client.Close();
    }
}
public class ChatServer
{
    static TcpListener tcpListener;
    List<ChatClient> clients = new List<ChatClient>();

    public void AddClient(ChatClient clientObject)
    {
        clients.Add(clientObject);
    }
    public void RemoveClient(string id)
    {
        ChatClient client = clients.FirstOrDefault(c => c.User.Id == id);

        if (client != null)
            clients.Remove(client);
    }

    public void Listen()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, 44433);
            tcpListener.Start();
            Console.WriteLine("\t\t Server is running.\n\t\t Waiting for connections...");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                var clientObject = new ChatClient(tcpClient, this);

                var clientThread = new Thread(new ThreadStart(clientObject.Start));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(String.Format("\t Error accepted! {0}", e.Message));
            Disconnect();
        }
    }

    public void BroadcastMessage(string message, string id)
    {
        byte[] data = Encoding.Unicode.GetBytes(message);
        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i].User.Id != id)
            {
                clients[i].Stream.Write(data, 0, data.Length);
            }
        }
    }
    public void Disconnect()
    {
        tcpListener.Stop();

        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].Close();
        }
        Environment.Exit(0);
    }
}

