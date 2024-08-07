using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using EncryptDecrypt.Models;
using EncryptDecrypt.Services;
using Microsoft.Win32;

namespace EncryptDecrypt.ViewModels
{
    public partial class ModelListViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<Model>? _models;
        [ObservableProperty] private int _progress;

        public ModelListViewModel()
        {
            Models = new ObservableCollection<Model>();

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Model>, string>(this,
                "ModelListViewModel_ModelAddToken", (sender, message) => { Models.Add(message.Value); });

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<Model>, string>(this,
                "ModelListViewModel_ModelRemoveToken", (sender, message) => { Models.Remove(message.Value); });

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<(Model, Model)>, string>(this,
                "ModelListViewModel_ModelUpdateToken", (sender, message) =>
                {
                    for (int i = 0; i < _models!.Count; i++)
                    {
                        if (_models[i] == message.Value.Item2)
                        {
                            _models[i] = message.Value.Item1;

                            break;
                        }
                    }
                });

            #region Test

#if DEBUG
            Models.Add(new()
            {
                Id = 42,
                Value = 1337,
                Name = "SampleModel"
            });

#endif

            #endregion
        }

        [RelayCommand]
        private void Add()
        {
            object lockerKey = new();
            OpenFileDialog openFile = new OpenFileDialog() {Filter = "Text|*.txt|All|*.*"};
            openFile.ShowDialog();
            FileInfo file = new(openFile.FileName);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>("OpenNewWindow")); 
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<FileInfo>(file)); 
            
        }
    }
}