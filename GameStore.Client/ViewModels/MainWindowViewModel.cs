using GameStore.Client.Service;

namespace GameStore.Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    private readonly GameApiService _gameApiService;
    
    public MainWindowViewModel(GameApiService gameApiService)
    {
        _gameApiService = gameApiService;
    }
}