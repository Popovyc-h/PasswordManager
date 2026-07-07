using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;
    
namespace PasswordManager.UI.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDerivedKeyService _derivedKeyService;
    private readonly ISessionService _sessionService;

    public event Action? OnLoginRequested;

    public RegisterViewModel(IUserRepository userRepository, IPasswordHasher passwordHasher, IDerivedKeyService derivedKeyService, ISessionService sessionService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _derivedKeyService = derivedKeyService;
        _sessionService = sessionService;
    }

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _masterPassword = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    public event Action? OnRegisterSuccess;

    [RelayCommand]
    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
            return;

        if (string.IsNullOrWhiteSpace(MasterPassword) || MasterPassword.Length < 8)
            return;

        if (MasterPassword != ConfirmPassword)
            return;

        if (await _userRepository.ExistsByUsernameAsync(Email))
            return;

        (string hash, string salt) = _passwordHasher.HashPassword(MasterPassword);

        var aesKeySalt = _derivedKeyService.GenerateSalt();

        var user = new User()
        {
            Username = Email,
            MasterPasswordHash = hash,
            MasterPasswordSalt = salt,
            AesKeySalt = aesKeySalt,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        _sessionService.AesKey = _derivedKeyService.DeriveKey(MasterPassword, user.AesKeySalt);
        _sessionService.UserId = user.Id;
        OnRegisterSuccess?.Invoke();
    }

    [RelayCommand]
    private void GoToLogin()
    {
        OnLoginRequested?.Invoke();
    }
}