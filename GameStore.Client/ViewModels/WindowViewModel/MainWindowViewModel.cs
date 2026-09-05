using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameStore.Client.Service;
using GameStore.Client.ViewModels.PageViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Client.ViewModels.WindowViewModel;

public partial class MainWindowViewModel : ViewModelBase
{

    private readonly IServiceProvider _serviceProvider;
    
    [ObservableProperty]
    private ViewModelBase? _selectedPageViewModel;
    
    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        OpenStorePage();
    }

    [RelayCommand]
    private void OpenStorePage()
    {
        SelectedPageViewModel = _serviceProvider.GetRequiredService<StorePageViewModel>();
    }

    [RelayCommand]
    private void OpenUserPage()
    {
        SelectedPageViewModel = _serviceProvider.GetRequiredService<UserPageViewModel>();
    }
}