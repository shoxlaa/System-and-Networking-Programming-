using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsFileReader
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        [ObservableProperty] private int _threadsCount;

        public MainViewModel()
        {


        }
        [RelayCommand]
        void Start()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            var result = dialog.ShowDialog();


            if (result.Value != null)
            {
                if (_threadsCount < 2 || _threadsCount > 8)
                {
                    return;
                }
                else
                {

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.ShowDialog();
                    Stream stream = File.Open(dialog.FileName, FileMode.OpenOrCreate);
                    ReadFile(stream, saveFileDialog.FileName);

                }

            }
        }
        void ReadFile(Stream stream, string FileName)
        {
            object locker = new();
            var reader = new BinaryReader(stream);
            var writer = new BinaryWriter(new FileStream($"{FileName}", FileMode.OpenOrCreate, FileAccess.Write));
            for (int i = 1; i < _threadsCount + 1; i++)
            {
                ThreadPool.QueueUserWorkItem((object c) =>
                {
                    int threadNum = Convert.ToInt32(c);
                    long bytesForOneThread = reader.BaseStream.Length / _threadsCount;
                    while (reader.BaseStream.Position < bytesForOneThread * threadNum)
                    {
                        lock (locker)
                        {
                            writer.Write(reader.ReadByte());
                        }

                    }


                }, i);

            }

        }


    }
}
