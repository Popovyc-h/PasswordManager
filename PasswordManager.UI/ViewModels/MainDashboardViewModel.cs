using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class MainDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPasswordEntryRepository _passwordEntryRepository;
    private readonly SettingsViewModel _settingsViewModel;
    private readonly AddEntryViewModel _addEntryViewModel;
    private readonly IEncryptionService _encryptionService;
    private readonly ISessionService _sessionService;

    private int _currentUserId;

    public MainDashboardViewModel(IPasswordGenerator passwordGenerator, IPasswordEntryRepository passwordEntryRepository, SettingsViewModel settingsViewModel, AddEntryViewModel addEntryViewModel, IEncryptionService encryptionService, ISessionService sessionService)
    {
        _passwordGenerator = passwordGenerator;
        _passwordEntryRepository = passwordEntryRepository;
        _settingsViewModel = settingsViewModel;
        _addEntryViewModel = addEntryViewModel;
        _encryptionService = encryptionService;
        _sessionService = sessionService;
        _addEntryViewModel.OnEntrySaved += OnEntryWasSaved;
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

    [RelayCommand]
    private void ShowAddEntry()
    {
        _addEntryViewModel.ResetForNewEntry();
        CurrentView = _addEntryViewModel;
    }

    public async Task InitializeAsync(int userId)
    {
        _currentUserId = userId;
        var listVm = new PasswordListViewModel(_passwordEntryRepository, _encryptionService, _sessionService);
        listVm.OnEditRequested += HandleEditRequested;
        await listVm.InitializeAsync(userId);
        CurrentView = listVm;
    }

    private async void OnEntryWasSaved()
    {
        await InitializeAsync(_currentUserId);
    }

    private void HandleEditRequested(PasswordEntry entry, string decryptedPassword)
    {
        _addEntryViewModel.LoadForEdit(entry, decryptedPassword);
        CurrentView = _addEntryViewModel;
    }
}