using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PasswordManager.UI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDarkTheme = true;

    partial void OnIsDarkThemeChanged(bool value)
    {
        if (Avalonia.Application.Current != null)
            Avalonia.Application.Current.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;
    }
}