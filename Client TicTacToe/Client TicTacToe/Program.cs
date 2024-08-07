
using System.Net;
using System.Net.Sockets;
TcpClientTicTacToe tictactoe = new("127.0.0.1", 44433);
tictactoe.Start();
tictactoe.@event.WaitOne();

public class TcpClientTicTacToe
{
    IPAddress _iP;
    int _port;
    public ManualResetEvent @event = new(false);
    TcpClient Client { get; set; }
    NetworkStream NetworkStream { get; set; }
    TicTacToeGame game;
    public TcpClientTicTacToe(string ipAdress, int port)
    {
        Client = new TcpClient(ipAdress, port);
        _iP = IPAddress.Parse(ipAdress);
        _port = port;
        game = new();
    }
    public void Start()
    {
        NetworkStream = Client.GetStream();
        StreamReader streamReader = new StreamReader(NetworkStream);
        StreamWriter streamWriter = new StreamWriter(NetworkStream)
        {
            AutoFlush = true
        };

        game.player = Convert.ToInt32(streamReader.ReadLine());

        while (true)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($" player : {game.player} ");
                Task.Factory.StartNew(() => {
                    while (true)
                    {
                        streamReader.Read(game.arr);
                    }
                }, TaskCreationOptions.LongRunning);

                game.Board();
            });

            //Console.WriteLine(streamReader.ReadLine()); 
            streamWriter.WriteLine(Console.ReadLine());

        }
    }
}
public class TicTacToeGame
{
    public char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    public int player = 1;
    public int choice;
    public int flag = 0;
    public void Board()
    {
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
        Console.WriteLine("     |     |      ");
    }
    public int CheckWin()
    {
        #region Horzontal Winning Condtion
        //Winning Condition For First Row
        if (arr[1] == arr[2] && arr[2] == arr[3])
        {
            return 1;
        }
        //Winning Condition For Second Row
        else if (arr[4] == arr[5] && arr[5] == arr[6])
        {
            return 1;
        }
        //Winning Condition For Third Row
        else if (arr[6] == arr[7] && arr[7] == arr[8])
        {
            return 1;
        }
        #endregion
        #region vertical Winning Condtion
        //Winning Condition For First Column
        else if (arr[1] == arr[4] && arr[4] == arr[7])
        {
            return 1;
        }
        //Winning Condition For Second Column
        else if (arr[2] == arr[5] && arr[5] == arr[8])
        {
            return 1;
        }
        //Winning Condition For Third Column
        else if (arr[3] == arr[6] && arr[6] == arr[9])
        {
            return 1;
        }
        #endregion
        #region Diagonal Winning Condition
        else if (arr[1] == arr[5] && arr[5] == arr[9])
        {
            return 1;
        }
        else if (arr[3] == arr[5] && arr[5] == arr[7])
        {
            return 1;
        }
        #endregion
        #region Checking For Draw
        // If all the cells or values filled with X or O then any player has won the match
        else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
        {
            return -1;
        }
        #endregion
        else
        {
            return 0;
        }
    }
}
