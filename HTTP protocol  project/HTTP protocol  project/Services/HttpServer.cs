using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_protocol__project.Services;

public class HttpServer
{
    public string Url { get; set; }
    HttpClient _client;
    HttpResponseMessage _response;

    public string FileName { get; set; }

    // 100 % - Lengh 
    // 1 %    - Lengh/100
    public long ReadedProcents { get; protected set; }
    public int _process = 0;
    public HttpServer()
    {
        _client = new HttpClient();
        _client.DefaultRequestVersion = HttpVersion.Version20;
    }
    public Task Read(int threadsCount)
    {
        return Task.Run(async () =>
        {
            _response = await _client.GetAsync(Url);
            var stream = await _response.Content.ReadAsStreamAsync();
            long readedBytes = 0;
            ThreadPool.QueueUserWorkItem((object v) =>
            {
                object locker = new();
                var reader = new BinaryReader(stream);
                var writer = new BinaryWriter(new FileStream($"{FileName}", FileMode.OpenOrCreate, FileAccess.Write));
                long BytesInOneProcent = reader.BaseStream.Length / 100;
                for (int i = 1; i < threadsCount + 1; i++)
                {
                    ThreadPool.QueueUserWorkItem((object c) =>
                    {
                        int threadNum = Convert.ToInt32(c);
                        long bytesForOneThread = reader.BaseStream.Length / threadsCount;
                        while (reader.BaseStream.Position < bytesForOneThread * threadNum)
                        {
                            lock (locker)
                            {
                                writer.Write(reader.ReadByte());

                                _process++;
                                if (BytesInOneProcent == _process)
                                {
                                    ReadedProcents++;
                                    _process = 0;
                                    Console.WriteLine(ReadedProcents);
                                }
                              
                            }

                        }


                    }, i);

                }
            });


        });



    }



public void Cancel()
    {
        _client.CancelPendingRequests();
    }


}