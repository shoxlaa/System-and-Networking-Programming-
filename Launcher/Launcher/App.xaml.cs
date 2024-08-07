using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
           string  CurrentProcces = Process.GetCurrentProcess().ProcessName;
            var allProcesses = Process.GetProcesses();
            if (allProcesses.Where(x=> x.ProcessName==CurrentProcces).Count()>1)
            {
                Environment.Exit(0);
            }

        }
    }
}
