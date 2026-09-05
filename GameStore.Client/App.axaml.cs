using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Net.Http;
using Avalonia.Markup.Xaml;
using GameStore.Client.Service;
using GameStore.Client.ViewModels;
using GameStore.Client.Views;
using Microsoft.Extensions.DependencyInjection;
using MainWindow = GameStore.Client.Views.Windows.MainWindow;
using MainWindowViewModel = GameStore.Client.ViewModels.WindowViewModel.MainWindowViewModel;
using GameStore.Client.ViewModels;
using GameStore.Client.ViewModels.PageViewModel;

namespace GameStore.Client;

public partial class App : Application
{

    public static IServiceProvider ServiceProvider { get; private set; } = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddSingleton(sp => new HttpClient 
        { 
            BaseAddress = new Uri("https://localhost:7183/api/v1/") 
        });


        serviceCollection.AddSingleton<GameApiService>();
        serviceCollection.AddSingleton<UserApiService>();

        serviceCollection.AddTransient<MainWindowViewModel>();
        
        serviceCollection.AddTransient<StorePageViewModel>();
        serviceCollection.AddTransient<UserPageViewModel>();
        
        ServiceProvider = serviceCollection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            
            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}