using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace PasswordManager.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action? OnLoginSuccess;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _masterPassword;

    [RelayCommand]
    private void Login()
    {

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(MasterPassword) || MasterPassword.Length < 6)
        {
            return;
        }

        OnLoginSuccess?.Invoke();
    }
}