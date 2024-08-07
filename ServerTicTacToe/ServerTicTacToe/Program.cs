using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

T tictactoe = new("127.0.0.1", 44433);
tictactoe.Start();
tictactoe.@event.WaitOne();

class T
{
    public ManualResetEvent @event = new ManualResetEvent(false);
    public TcpListener Server { get; set; }
    public TcpClient Client { get; set; }
    public NetworkStream NetworkStream { get; set; }
    public StreamReader StreamReader { get; set; }
    public StreamWriter StreamWriter { get; set; }
    public ConcurrentBag<TcpClient> tcpClients { get; set; }
    public string Message { get; set; }
    TicTacToeGame gameLogic { get; set; }

    public T(string ipAdress, int port)
    {
        Server = new(IPAddress.Parse(ipAdress), port);
        tcpClients = new();
        gameLogic = new();
    }
    public string Read()
    {
        try
        {
            var l = StreamReader.ReadLine();
            return l;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void Write(string message)
    {
        StreamWriter.WriteLine(message);
    }
    public void Start()
    {

        Server.Start();
        Console.WriteLine("Starting server...");
        Console.WriteLine("Server started!");
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for a client...");
                Client = Server.AcceptTcpClient();
                Console.WriteLine("Client accepted!");
                tcpClients.Add(Client);
                NetworkStream = Client.GetStream();
                StreamWriter = new(NetworkStream) { AutoFlush = true };
                StreamReader = new(NetworkStream);
                StreamWriter.WriteLine(tcpClients.Count);
                if (tcpClients.Count < 2)
                {
                    continue;
                }

                foreach (var c in tcpClients)
                {
                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {

                            NetworkStream = c.GetStream();
                            StreamWriter = new(NetworkStream) { AutoFlush = true };
                            StreamReader = new(NetworkStream);

                            Task.Factory.StartNew(() =>
                            {
                                StreamWriter.Write(gameLogic.arr);
                            });

                            Message = StreamReader.ReadLine();
                            if (Message == null)
                            {
                                return;
                            }
                            Console.WriteLine(Message);
                            Console.WriteLine($"Player {gameLogic.player}: {Message}");
                            gameLogic.choice = Convert.ToInt32(Message);
                            if (gameLogic.arr[gameLogic.choice] != 'X' && gameLogic.arr[gameLogic.choice] != 'O')
                            {
                                if (gameLogic.player % 2 == 0) //if chance is of player 2 then mark O else mark X
                                {
                                    gameLogic.arr[gameLogic.choice] = 'O';
                                    gameLogic.player++;
                                }
                                else
                                {
                                    gameLogic.arr[gameLogic.choice] = 'X';
                                    gameLogic.player++;
                                }
                            }
                            else
                            //If there is any possition where user want to run
                            //and that is already marked then show message and load board again
                            {
                                Console.WriteLine("Sorry the row {0} is already marked with {1}", gameLogic.choice, gameLogic.arr[gameLogic.choice]);
                                Console.WriteLine("\n");
                                Console.WriteLine("Please wait 2 second board is loading again.....");
                                Thread.Sleep(2000);
                            }

                            gameLogic.flag = gameLogic.CheckWin();// calling of check win 
                                                                  // This loop will be run until all cell of the grid is not marked
                                                                  //with X and O or some player is not win

                            //отпраить доску   
                            //Task.Factory.StartNew(() =>
                            //{
                            //    while (true)
                            //    {
                            //        StreamWriter.Write(gameLogic.arr);
                            //    }
                            //}, TaskCreationOptions.LongRunning);
                            // getting filled board again 
                            // streamWriter.Write(gameLogic.arr);
                            if (gameLogic.flag == 1)
                            // if flag value is 1 then someone has win or
                            //means who played marked last time which has win
                            {
                                Console.WriteLine("Player {0} has won", (gameLogic.player % 2) + 1);
                            }
                            else// if flag value is -1 the match will be draw and no one is winner
                            {
                                Console.WriteLine("Draw");
                            }
                        }
                    });
                }


            }

        }
        catch (Exception ex) when (ex is SocketException or ObjectDisposedException)
        {
            global::System.Console.WriteLine("Client disconnected");
        }
        finally
        {
            Client.Dispose();
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
