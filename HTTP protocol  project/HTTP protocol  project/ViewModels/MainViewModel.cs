using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using HTTP_protocol__project.Services;

namespace HTTP_protocol__project.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private BaseViewModel? _currentViewModel;
    private readonly ViewModelFactory _factory;

    public MainViewModel(ViewModelFactory factory)
    {
        _factory = factory;

        WeakReferenceMessenger.Default.Register<ValueChangedMessage<ViewModelCreator>>(this,(sender, message) =>
        {
            CurrentViewModel = _factory.Create(message.Value);
        });

        WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ViewModelCreator>(new DowladingViewModelCreator()));
    }
}