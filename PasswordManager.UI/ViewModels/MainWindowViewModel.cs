using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Core.Entities;

namespace PasswordManager.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;
    public int CurrentUserId { get; set; }
    private readonly MainDashboardViewModel _dashboardViewModel;

    public byte[]? AesKey { get; set; }

    public MainWindowViewModel(LoginViewModel loginPage, MainDashboardViewModel dashboardViewModel)
    {
        _dashboardViewModel = dashboardViewModel;
        loginPage.OnLoginSuccess += MoveToMainPage;
        CurrentPage = loginPage;
    }

    private async void MoveToMainPage(byte[] aesKey, int userId)
    {
        if (CurrentPage is LoginViewModel oldLogin)
            oldLogin.OnLoginSuccess -= MoveToMainPage;

        CurrentUserId = userId;
        AesKey = aesKey;
        CurrentPage = _dashboardViewModel;
        await _dashboardViewModel.InitializeAsync(userId);
    }
}