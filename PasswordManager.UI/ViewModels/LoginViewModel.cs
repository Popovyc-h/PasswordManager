using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action<byte[]>? OnLoginSuccess;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDerivedKeyService _derivedKeyService;

    public LoginViewModel(IUserRepository userRepository, IPasswordHasher passwordHasher, IDerivedKeyService derivedKeyService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _derivedKeyService = derivedKeyService;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _masterPassword;

    [RelayCommand]
    private async Task Login()
    {

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
            return;

        if (string.IsNullOrWhiteSpace(MasterPassword) || MasterPassword.Length < 6)
            return;

        var user = await _userRepository.GetByUsernameAsync(Email);

        if (user == null)
            return;

        var isPasswordValid = _passwordHasher.VerifyPassword(MasterPassword, user.MasterPasswordHash, user.MasterPasswordSalt);

        if (isPasswordValid)
        {
            var aesKey = _derivedKeyService.DeriveKey(MasterPassword, user.AesKeySalt);

            OnLoginSuccess?.Invoke(aesKey);
        }
    }
}