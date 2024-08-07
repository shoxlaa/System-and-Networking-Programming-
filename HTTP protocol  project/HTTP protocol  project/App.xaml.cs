using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HTTP_protocol__project.Services;
using HTTP_protocol__project.ViewModels;
using HTTP_protocol__project.Views;
using SimpleInjector; 


namespace HTTP_protocol__project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new Container();

            container.Register<ViewModelFactory>(Lifestyle.Singleton);
            container.Collection.Register<BaseViewModel>(new[] { typeof(DownloadViewModel), typeof(MainViewModel) }, Lifestyle.Singleton);

            // Register other services...
			
            // View start
            var factory = container.GetInstance<ViewModelFactory>();
            var view = new MainWindow()
            {
                DataContext = factory.Create(new MainViewModelCreator())
            };

            view.Show();

            base.OnStartup(e);
        }
    }
}
