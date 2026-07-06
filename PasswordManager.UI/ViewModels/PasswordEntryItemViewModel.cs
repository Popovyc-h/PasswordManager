using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System;

namespace PasswordManager.UI.ViewModels;

public partial class PasswordEntryItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private PasswordEntry _entry;

    private readonly IEncryptionService _encryptionService;
    private readonly ISessionService _sessionService;

    public event Action<PasswordEntry, string>? OnEditRequested;

    public PasswordEntryItemViewModel(PasswordEntry passwordEntry, IEncryptionService encryptionService, ISessionService sessionService)
    {
        Entry = passwordEntry;
        _encryptionService = encryptionService;
        _sessionService = sessionService;
    }

    [ObservableProperty]
    private bool _isRevealed;

    [ObservableProperty]
    private string? _decryptedPassword;

    [RelayCommand]
    public void ToggleReveal()
    {
        if (IsRevealed)
        {
            IsRevealed = false;
            DecryptedPassword = null;
        }
        else
        {
            DecryptedPassword = DecryptCurrentPassword();
            IsRevealed = true;
        }
    }

    [RelayCommand]
    private void EditEntry()
    {
        var decrypted = DecryptCurrentPassword();
        if (decrypted == null) return;

        OnEditRequested?.Invoke(Entry, decrypted);
    }

    private string? DecryptCurrentPassword()
    {
        if (_sessionService.AesKey == null)
            return null;

        return _encryptionService.Decrypt(Entry.EncryptedPassword, Entry.IV, _sessionService.AesKey);
    }
}