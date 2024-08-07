using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EncryptDecrypt.ViewModels
{
    public partial class EncryptingViewModel : BaseViewModel
    {
        [ObservableProperty] private string _text;
        [ObservableProperty] private double _progress;
        FileInfo _file;
        public EncryptingViewModel()
        {
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<FileInfo>>(this,
                (sender, message) =>
                {
                    if (message != null && _file == null)
                    {
                        _file = message.Value;
                        Text = File.ReadAllText(_file.FullName);
                    }
                });
            if (_file != null)
            {
                ThreadPool.QueueUserWorkItem((object value) =>
                {
                    File.AppendAllText($"{_file.FullName.Remove(_file.FullName.Length - 3)}ff", Text);
                });
            }
        }
        [RelayCommand]
        void StartEncrypting()
        {
            long Lengh = _file.Length;
            double InOneChar = Lengh / 100;
            object lockerKey = new();
            ThreadPool.QueueUserWorkItem((object value) =>
            {
                Progress = 0;
                string newVal = "";
                lock (lockerKey)
                {
                    foreach (var c in Text)
                    {
                        newVal += (char)((int)c + 1);
                        Progress += InOneChar;
                    }
                    Text = newVal;
                }
            }, ApartmentState.STA);
            Progress = 100;
        }


    }
}