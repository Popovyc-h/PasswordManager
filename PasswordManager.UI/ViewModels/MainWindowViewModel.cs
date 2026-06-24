using CommunityToolkit.Mvvm.ComponentModel;

namespace PasswordManager.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;

    public byte[]? AesKey { get; set; }

    public MainWindowViewModel(LoginViewModel loginPage)
    {
        loginPage.OnLoginSuccess += MoveToMainPage;

        CurrentPage = loginPage;
    }

    private void MoveToMainPage(byte[] aesKey)
    {
        if (CurrentPage is LoginViewModel oldLogin)
            oldLogin.OnLoginSuccess -= MoveToMainPage;

        AesKey = aesKey;
        CurrentPage = new MainDashboardViewModel();
    }
}