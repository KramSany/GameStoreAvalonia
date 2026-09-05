using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GameStore.Client.DTOs;
using GameStore.Client.Service;
using Avalonia.Threading;


namespace GameStore.Client.ViewModels.PageViewModel;

public partial class UserPageViewModel : ViewModelBase
{
    private readonly UserApiService _userApiService;

    [ObservableProperty] private ObservableCollection<UserDto> _users = new();

    public UserPageViewModel(UserApiService userApiService)
    {
        _userApiService = userApiService;
        LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        try
        {
            var result = await _userApiService.GetUsersAsync();
            
            Dispatcher.UIThread.Post(() =>
            {
                Users.Clear();
                foreach (var user in result)
                {
                    Users.Add(user);
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки каталога: {ex.Message}");
        }
    }

}