using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameStore.Client.Service;
using GameStore.Client.ViewModels.PageViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Client.ViewModels.WindowViewModel;

public partial class MainWindowViewModel : ViewModelBase
{

    private readonly GameApiService _gameApiService;
    
    [ObservableProperty]
    private ViewModelBase? _selectedPageViewModel;
    
    public MainWindowViewModel(GameApiService gameApiService)
    {
        _gameApiService = gameApiService;
        OpenStorePage();
    }

    [RelayCommand]
    private void OpenStorePage()
    {
        SelectedPageViewModel = new StorePageViewModel(_gameApiService);
    }
}