using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    // изначально хотела использовать это но что бы не выходить за рамки паттерна не использовала
    // (юзала microsoft begaviours код получился не очень читабельным)

    //private const int GWL_STYLE = -16;
    //private const int WS_SYSMENU = 0x80000;
    //[DllImport("user32.dll", SetLastError = true)]
    //private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    //[DllImport("user32.dll")]
    //private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong); 

    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        [ObservableProperty]
        string _currentProcces;
        [ObservableProperty]
        string _parametr1;
        [ObservableProperty]
        string _parametr2;
        [ObservableProperty]
        string _operator;
        [ObservableProperty]
        List<string> _op;
        public MainViewModel()
        {
            CurrentProcces = Process.GetCurrentProcess().ProcessName;
            Op = new() { "+", "-", "/", "*"};
        }
        [ICommand]
        void Luanch()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo(@"C:\Users\Legion\source\repos\SimpleCalculator\SimpleCalculator\bin\Debug\net6.0\SimpleCalculator.exe", $"{Parametr1}{Operator}{Parametr2}")
                
            };

            process.Start();
        }
    }
}
