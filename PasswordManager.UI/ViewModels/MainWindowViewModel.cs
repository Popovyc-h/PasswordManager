using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentPage;
    
    private readonly MainDashboardViewModel _dashboardViewModel;
    private readonly ISessionService _sessionService;
    private readonly RegisterViewModel _registerPage;
    private readonly LoginViewModel _loginPage;

    public MainWindowViewModel(LoginViewModel loginPage, MainDashboardViewModel dashboardViewModel, ISessionService sessionService, RegisterViewModel registerPage, LoginViewModel loginViewModel)
    {
        _dashboardViewModel = dashboardViewModel;
        _sessionService = sessionService;
        _registerPage = registerPage;
        _loginPage = loginPage;

        loginPage.OnLoginSuccess += MoveToMainPage;
        CurrentPage = loginPage;
        loginPage.OnRegisterRequested += MoveToRegisterPage;
        registerPage.OnRegisterSuccess += MoveToMainPage;
        registerPage.OnLoginRequested += MoveToLoginPage;
    }

    private async void MoveToMainPage()
    {
        if (CurrentPage is LoginViewModel oldLogin)
            oldLogin.OnLoginSuccess -= MoveToMainPage;

        if (CurrentPage is RegisterViewModel oldRegister)
            oldRegister.OnRegisterSuccess -= MoveToMainPage;

        CurrentPage = _dashboardViewModel;
        await _dashboardViewModel.InitializeAsync(_sessionService.UserId);
    }

    private async void MoveToRegisterPage()
    {
        CurrentPage = _registerPage;
    }

    private void MoveToLoginPage()
    {
        CurrentPage = _loginPage;
    }
}