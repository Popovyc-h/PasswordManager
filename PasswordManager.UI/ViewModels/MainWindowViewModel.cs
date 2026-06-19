using CommunityToolkit.Mvvm.ComponentModel;

namespace PasswordManager.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;


    public MainWindowViewModel()
    {
        var loginPage = new LoginViewModel();

        loginPage.OnLoginSuccess += MoveToMainPage;

        CurrentPage = loginPage;
    }

    private void MoveToMainPage()
    {
        if (CurrentPage is LoginViewModel oldLogin)
            oldLogin.OnLoginSuccess -= MoveToMainPage;

        CurrentPage = new MainDashboardViewModel();
    }
}