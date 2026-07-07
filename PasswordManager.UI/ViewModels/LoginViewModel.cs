using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action? OnLoginSuccess;
    public event Action? OnRegisterRequested;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDerivedKeyService _derivedKeyService;
    private readonly ISessionService _sessionService;

    public LoginViewModel(IUserRepository userRepository, IPasswordHasher passwordHasher, IDerivedKeyService derivedKeyService, ISessionService sessionService)
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

    [NotifyPropertyChangedFor(nameof(PasswordMaskChar))]
    [ObservableProperty]
    private bool _isPasswordVisible;
    
    public char PasswordMaskChar => IsPasswordVisible ? '\0' : '*';

    [RelayCommand]
    private async Task Login()
    {

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
            return;

        if (string.IsNullOrWhiteSpace(MasterPassword) || MasterPassword.Length < 8)
            return;

        var user = await _userRepository.GetByUsernameAsync(Email);

        if (user == null)
            return;

        var isPasswordValid = _passwordHasher.VerifyPassword(MasterPassword, user.MasterPasswordHash, user.MasterPasswordSalt);

        if (isPasswordValid)
        {
            _sessionService.AesKey = _derivedKeyService.DeriveKey(MasterPassword, user.AesKeySalt);
            _sessionService.UserId = user.Id;
            OnLoginSuccess?.Invoke();
        }
    }

    [RelayCommand]
    private void ToggleShowPassword()
    {
        if (IsPasswordVisible)
            IsPasswordVisible = false;
        else
            IsPasswordVisible = true;
    }

    [RelayCommand] 
    private void GoToRegister() 
    { 
        OnRegisterRequested?.Invoke(); 
    }
}