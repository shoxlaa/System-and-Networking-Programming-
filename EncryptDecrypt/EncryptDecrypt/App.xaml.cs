using System.Windows;

using EncryptDecrypt.Services;
using EncryptDecrypt.ViewModels;
using EncryptDecrypt.Views;

using SimpleInjector;

namespace EncryptDecrypt
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
			container.Collection.Register<BaseViewModel>(new[] { typeof(ModelListViewModel), typeof(MainViewModel) }, Lifestyle.Singleton);

			// Register other services...
			
			// View start
			var factory = container.GetInstance<ViewModelFactory>();
			var view = new MainView
			{
				DataContext = factory.Create(new MainViewModelCreator())
			};
			

			view.Show();

			base.OnStartup(e);
		}
	}
}
