using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using EncryptDecryptMvvm.Services;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace EncryptDecryptMvvm.ViewModels
{
    public partial class StartViewModel : BaseViewModel 
    {
        [RelayCommand] 
        void Start()
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("new"));

        }

        [RelayCommand]
        private void Add()
        {
            object lockerKey = new();
            OpenFileDialog openFile = new OpenFileDialog() { Filter = "Text|*.txt|All|*.*" };
            openFile.ShowDialog();
            FileInfo file = new(openFile.FileName);

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<FileInfo>(file));

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("new"));
        }

    }

}
