using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.UI.ViewModels;

public partial class MainDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    private readonly IPasswordGenerator _passwordGenerator;
    
    public MainDashboardViewModel(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
        _currentView = new PasswordListViewModel();
    }

    [RelayCommand]
    private void ShowGenerator()
    {
        CurrentView = new PasswordGeneratorViewModel(_passwordGenerator);
    }

    [RelayCommand]
    private void ShowPasswordList()
    {
        CurrentView = new PasswordListViewModel();
    }

    [RelayCommand]
    private void ShowSettings()
    {
        CurrentView = new SettingsViewModel();
    }
}