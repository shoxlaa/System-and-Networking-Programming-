using System.Reflection;
using System.Runtime.Loader;
using System.IO;
using System.Windows.Forms;
using System.Text;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        System.Text.Encoding InputEncoding = Encoding.UTF8;
        bool flag = true;
        var assemblyLoadContext = new AssemblyLoadContext("MyProgram", true);
        assemblyLoadContext.Unloading += (context) =>
        {
            Console.WriteLine("Assembly unloaded");
        };
        while (flag)
        {
            Console.WriteLine("1. Calculator \n 2. OpenFile \n 0.stop");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    {
                        var assembly = assemblyLoadContext.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "CalculatorLib"));
                        Type? type = assembly.GetType("CalculatorLib.Calculator");

                        if (type != null)
                        {
                            try
                            {
                                var method = type.GetMethod("Calculate", new Type[] { typeof(int), typeof(char), typeof(int) });
                                var instance = Activator.CreateInstance(type);
                                Console.Write("Parametr1 :");
                                int parametr1 = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Operator + - / * :");
                                char @operator =Convert.ToChar(Console.ReadLine());
                                Console.Write("Parametr2 :");
                                int parametr2 = Convert.ToInt32(Console.ReadLine());
                                if (parametr1 != null && parametr2 != null && parametr2 != 0)
                                {
                                    var result = method?.Invoke(instance, new object[] { parametr1, @operator, parametr2 });

                                    if (result is int output)
                                    {
                                        Console.WriteLine($"Result {output}");

                                    }
                                }
                            }
                            catch { continue; }
                        }
                    }
                    break;
                case "2":
                    {
                        var assembly = assemblyLoadContext.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "System.Windows.Forms.dll"));
                        Type? type = assembly.GetType("System.Windows.Forms.OpenFileDialog");
                        if (type != null)
                        {
                            //filter 
                            var param1 = type.GetProperty("Filter");
                            var instance = Activator.CreateInstance(type);
                            param1.SetValue(instance, "Text|*.txt|All|*.*");
                            //show dialog 
                            var showDialog = type.GetMethod("ShowDialog", new Type[] { });
                            showDialog?.Invoke(instance, null);
                            // getName
                            var fileName = type.GetProperty("FileName", new Type[] { });
                            var _fileName = fileName?.GetValue(instance);
                            // 
                            Console.WriteLine(Convert.ToString(_fileName));

                            string fileContent = string.Empty;
                            var openFile = type.GetMethod("OpenFile", new Type[] { });
                            var fileStream = openFile?.Invoke(instance, null);

                            using (StreamReader reader = new StreamReader((Stream)fileStream))
                            {
                                fileContent = reader.ReadToEnd();
                            }
                            Console.WriteLine(fileContent);
                        }
                    }
                    break;
                case "0":
                    {
                        flag = false;
                        assemblyLoadContext.Unload();
                    }
                    break;
            }
        }
    }
}
