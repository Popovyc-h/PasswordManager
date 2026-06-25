using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Interfaces;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class PasswordGeneratorViewModel : ViewModelBase
{
    private readonly IPasswordGenerator _passwordGenerator;

    [ObservableProperty] 
    private string _generatedPassword = string.Empty;
    
    [ObservableProperty] 
    private int _passwordLength = 16;
    
    [ObservableProperty] 
    private bool _useUppercase = true;
    
    [ObservableProperty] 
    private bool _useLowercase = true;
    
    [ObservableProperty] 
    private bool _useDigits = true;
    
    [ObservableProperty] 
    private bool _useSpecial = false;

    public PasswordGeneratorViewModel(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
    }

    [RelayCommand]
    public void Generate()
    {
        GeneratedPassword = _passwordGenerator.Generate(PasswordLength, UseUppercase, UseLowercase, UseDigits, UseSpecial);
    }

    [RelayCommand]
    public async Task Copy()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var clipboard = desktop.MainWindow?.Clipboard;

            if (clipboard != null && !string.IsNullOrEmpty(GeneratedPassword))
                await clipboard.SetTextAsync(GeneratedPassword);
        }
    }
}