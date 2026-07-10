using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isDarkTheme = true;

    private readonly ISessionService _sessionService;
    private readonly IRepository<UserSettings> _userSettingsRepository;

    public SettingsViewModel(ISessionService sessionService, IRepository<UserSettings> userSettingsRepository)
    {
        _sessionService = sessionService;
        _userSettingsRepository = userSettingsRepository;
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        if (Avalonia.Application.Current != null)
            Avalonia.Application.Current.RequestedThemeVariant = value ? ThemeVariant.Dark : ThemeVariant.Light;

        _ = SaveThemeAsync(value ? "Dark" : "Light");
    }

    private async Task SaveThemeAsync(string theme)
    {
        var allSettings = await _userSettingsRepository.GetAllAsync();
        var settings = allSettings.FirstOrDefault(s => s.UserId == _sessionService.UserId);

        if (settings == null)
            return;

        settings.Theme = theme;
        await _userSettingsRepository.UpdateAsync(settings);
    }
}