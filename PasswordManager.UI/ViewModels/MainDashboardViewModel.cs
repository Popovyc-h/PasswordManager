using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class MainDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPasswordEntryRepository _passwordEntryRepository;
    private readonly SettingsViewModel _settingsViewModel;

    private int _currentUserId;

    public MainDashboardViewModel(IPasswordGenerator passwordGenerator, IPasswordEntryRepository passwordEntryRepository, SettingsViewModel settingsViewModel)
    {
        _passwordGenerator = passwordGenerator;
        _passwordEntryRepository = passwordEntryRepository;
        _settingsViewModel = settingsViewModel;
        _currentView = new PasswordListViewModel(_passwordEntryRepository);
    }

    [RelayCommand]
    private void ShowGenerator()
    {
        CurrentView = new PasswordGeneratorViewModel(_passwordGenerator);
    }

    [RelayCommand]
    private async Task ShowPasswordList()
    {
        await InitializeAsync(_currentUserId);
    }

    [RelayCommand]
    private void ShowSettings()
    {
        CurrentView = _settingsViewModel;
    }

    public async Task InitializeAsync(int userId)
    {
        var listVm = new PasswordListViewModel(_passwordEntryRepository);
        await listVm.InitializeAsync(userId);
        CurrentView = listVm;
    }
}