using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;
    
    private readonly MainDashboardViewModel _dashboardViewModel;
    private readonly ISessionService _sessionService;

    public MainWindowViewModel(LoginViewModel loginPage, MainDashboardViewModel dashboardViewModel, ISessionService sessionService)
    {
        _dashboardViewModel = dashboardViewModel;
        _sessionService = sessionService;
        loginPage.OnLoginSuccess += MoveToMainPage;
        CurrentPage = loginPage;
    }

    private async void MoveToMainPage()
    {
        if (CurrentPage is LoginViewModel oldLogin)
            oldLogin.OnLoginSuccess -= MoveToMainPage;

        CurrentPage = _dashboardViewModel;
        await _dashboardViewModel.InitializeAsync(_sessionService.UserId);
    }
}