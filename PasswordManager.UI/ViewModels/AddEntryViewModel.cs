using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class AddEntryViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _login = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private string _notes = string.Empty;

    [ObservableProperty]
    private int _categoryId = 1;

    [ObservableProperty]
    private int _length = 16;

    [ObservableProperty]
    private bool _useUppercase;

    [ObservableProperty]
    private bool _useLowercase;

    [ObservableProperty]
    private bool _useDigits;

    [ObservableProperty]
    private bool _useSpecial;

    private readonly IPasswordEntryRepository _passwordEntryRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly ISessionService _sessionService;

    public event Action? OnEntrySaved;

    public AddEntryViewModel(IPasswordEntryRepository passwordEntryRepository, IEncryptionService encryptionService, IPasswordGenerator passwordGenerator, ISessionService sessionService)
    {
        _passwordEntryRepository = passwordEntryRepository;
        _encryptionService = encryptionService;
        _passwordGenerator = passwordGenerator;
        _sessionService = sessionService;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (_sessionService.AesKey == null)
            return;

        var (encryptedPassword, iv) = _encryptionService.Encrypt(Password, _sessionService.AesKey);

        var passwordEntry = new PasswordEntry
        {
            UserId = _sessionService.UserId,
            Title = Title,
            Login = Login,
            EncryptedPassword = encryptedPassword,
            IV = iv,
            Url = Url,
            Notes = Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = CategoryId,
        };

        await _passwordEntryRepository.AddAsync(passwordEntry);
        OnEntrySaved?.Invoke();
    }

    [RelayCommand]
    private void GeneratePassword()
    {
        Password = _passwordGenerator.Generate(Length, UseUppercase, UseLowercase, UseDigits, UseSpecial);
    }
}