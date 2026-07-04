using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;

namespace PasswordManager.UI.ViewModels;

public partial class PasswordEntryItemViewModel : ViewModelBase
{
    [ObservableProperty]
    private PasswordEntry _entry;

    private readonly IEncryptionService _encryptionService;
    private readonly ISessionService _sessionService;

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
        if (_sessionService.AesKey == null)
            return;

        if (IsRevealed)
        {
            IsRevealed = false;
            DecryptedPassword = null;
        }
        else
        {
            DecryptedPassword = _encryptionService.Decrypt(Entry.EncryptedPassword, Entry.IV, _sessionService.AesKey);
            IsRevealed = true;
        }
    }
}