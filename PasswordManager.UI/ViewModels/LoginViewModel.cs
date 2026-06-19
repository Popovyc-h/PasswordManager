using CommunityToolkit.Mvvm.Input;
using System;

namespace PasswordManager.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    public event Action? OnLoginSuccess;

    [RelayCommand]
    private void Login()
    {
        OnLoginSuccess?.Invoke();
    }
}