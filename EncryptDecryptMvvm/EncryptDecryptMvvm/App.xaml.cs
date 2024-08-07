using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using EncryptDecryptMvvm.Services;
using EncryptDecryptMvvm.ViewModels;
using EncryptDecryptMvvm.Views;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EncryptDecryptMvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container p_container { get; } = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            p_container.Options.EnableAutoVerification = false;
            p_container.RegisterSingleton<MainViewModel>();
            p_container.RegisterSingleton<EncryptingViewModel>();
            p_container.RegisterSingleton<StartViewModel>();
            // p_container.RegisterSingleton<IStoreDataBase, StoreDataBase>();
            p_container.RegisterSingleton<ViewModelFactory>();
            p_container.RegisterSingleton<MainWindow>();
            p_container.RegisterSingleton<EncryptingView>();

            var window = p_container.GetInstance<MainWindow>();
            window.Show();


            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (sender, message) =>
            {
                var window2 = p_container.GetInstance<EncryptingView>();
                window2.Show(); 

            });




            base.OnStartup(e);
        }
    }
}
