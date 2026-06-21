using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action? OnLoginSuccess;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginViewModel(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _masterPassword;

    [RelayCommand]
    private async Task Login()
    {

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(MasterPassword) || MasterPassword.Length < 6)
        {
            return;
        }

        var user = await _userRepository.GetByUsernameAsync(Email);

        if (user == null)
            return;

        var isPasswordValid = _passwordHasher.VerifyPassword(MasterPassword, user.MasterPasswordHash, user.MasterPasswordSalt);

        if (isPasswordValid)
            OnLoginSuccess?.Invoke();
    }
}