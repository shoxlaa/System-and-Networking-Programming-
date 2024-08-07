using System;
using System.IO;
using System.Runtime.Loader;
using System.Windows.Forms;
using System.Windows;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var assemblyLoadContext = new AssemblyLoadContext("MyProgram", true);

            assemblyLoadContext.Unloading += (context) =>
            {
                Console.WriteLine("Assembly unloaded");
            };
            var assembly = assemblyLoadContext.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "System.Windows.Forms.dll"));
            foreach (var item in assemblyLoadContext.Assemblies)
            {
                Console.WriteLine(item.FullName);
            }
            Type? type = assembly.GetType("System.Windows.Forms.OpenFileDialog");
            if (type != null)
            {
                var method = type.GetMethod("ShowDialog", new Type[] { });
                var instance = Activator.CreateInstance(type);
                var result = method?.Invoke(instance, new object[] { });

                if (result is int output)
                {
                    Console.WriteLine(output);

                    assemblyLoadContext.Unload();
                }
            }
        }
    }
}
