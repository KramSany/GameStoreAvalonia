using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameStore.Client.DTOs;
using GameStore.Client.Service;
using Avalonia.Threading;

namespace GameStore.Client.ViewModels.PageViewModel;

public partial class StorePageViewModel : ViewModelBase
{
    private readonly GameApiService _gameApiService;

    [ObservableProperty] private ObservableCollection<GameDto> _games = new();
    [ObservableProperty] private ObservableCollection<GameDetailsDto> _game = new();


    public StorePageViewModel(GameApiService gameApiService)
    {
        _gameApiService = gameApiService;
        LoadGamesAsync();
    }

    private async Task LoadGamesAsync()
    {
        try
        {
            var result = await _gameApiService.GetGamesAsync();
            
            Dispatcher.UIThread.Post(() =>
            {
                Games.Clear();
                foreach (var user in result)
                {
                    Games.Add(user);
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки каталога: {ex.Message}");
        }
    }

}