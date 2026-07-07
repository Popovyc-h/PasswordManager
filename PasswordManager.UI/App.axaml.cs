using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Services;
using PasswordManager.Data.Data;
using PasswordManager.Data.Repositories;
using PasswordManager.UI.ViewModels;
using PasswordManager.UI.Views;
using System.Linq;

namespace PasswordManager.UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();

            services.AddTransient<AppDbContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<LoginViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddSingleton<IDerivedKeyService, DerivedKeyService>();
            services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
            services.AddTransient<MainDashboardViewModel>();
            services.AddTransient<IPasswordEntryRepository, PasswordEntryRepository>();
            services.AddTransient<SettingsViewModel>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddTransient<AddEntryViewModel>();
            services.AddTransient<RegisterViewModel>();

            var serviceProvider = services.BuildServiceProvider();

            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
            BindingPlugins.DataValidators.Remove(plugin);
    }
}