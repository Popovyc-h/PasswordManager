using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PasswordManager.UI.ViewModels;

public partial class MainDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    public MainDashboardViewModel()
    {
        _currentView = new PasswordListViewModel();
    }

    [RelayCommand]
    private void ShowGenerator()
    {
        CurrentView = new PasswordGeneratorViewModel();
    }

    [RelayCommand]
    private void ShowPasswordList()
    {
        CurrentView = new PasswordListViewModel();
    }
}