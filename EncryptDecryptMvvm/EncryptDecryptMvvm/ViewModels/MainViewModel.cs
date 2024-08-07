using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using EncryptDecryptMvvm.Services;

namespace EncryptDecryptMvvm.ViewModels
{

    public partial class MainViewModel : BaseViewModel
	{
		private readonly ViewModelFactory _factory;

		[ObservableProperty]
		private BaseViewModel? _currentViewModel;

		public MainViewModel(ViewModelFactory factory)
		{
			_factory = factory;

			CurrentViewModel = (BaseViewModel?)factory.Create(ViewModelType.StartViewModel);

			WeakReferenceMessenger.Default.Register<ValueChangedMessage<ViewModelType>>(this, (sender, message) =>
			{
				CurrentViewModel = (BaseViewModel?)_factory.Create(message.Value);
			});
		}
	}

}
