long num = 0;
long theBiggestNum = 6_000_000;
long numToAdd = 500_000;
object loker = new object();
int threadsCount = 3;
int threadNext = 0;

for (int i = 0; i < threadsCount; i++)
{
    ThreadPool.QueueUserWorkItem((objectState) =>
    {
        int v = 0;
        while (v < theBiggestNum / threadsCount / numToAdd)
        {
            v++;
            if (num != theBiggestNum)
            {
                int threadNum = Convert.ToInt32(objectState);
                lock (loker)
                {
                    if (threadNext == threadNum)
                    {
                        num += numToAdd;
                        Console.WriteLine($"Thread{threadNum}  {num}");
                        threadNext++;
                        if (threadNext > 2)
                        {
                            threadNext = 0;
                        }
                        continue;
                    }
                    v--;
                }
            }
        }
    }, i);
}


Console.ReadLine();

































//File.WriteAllText("thx.pp", "abib");
//File.WriteAllText("thg.txt", "abobab");
//FileInfo fileInfo = new("thx.txt"); 
//FileInfo fileInfo2 = new("thg.txt");

////File.Open("thx.pp", FileMode.OpenOrCreate);
////File.Open("thg.txt", FileMode.OpenOrCreate);
//File.Delete("thx.pp");


////string val =File.ReadAllText(fileInfo.FullName);

////Console.WriteLine(val);
////int Progress = 0;
////string newVal = "";

////foreach (var c in val)
////{
////    newVal+=(char)((int)c+1 ); 
////    Progress++;
////}
////Console.WriteLine(newVal);

////File.WriteAllText($"{fileInfo.FullName.Remove(fileInfo.FullName.Length-2)}enc", "not pups");