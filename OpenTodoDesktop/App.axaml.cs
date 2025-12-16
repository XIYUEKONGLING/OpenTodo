using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using OpenTodoDesktop.Localization;
using OpenTodoDesktop.Services;
using OpenTodoDesktop.ViewModels;
using OpenTodoDesktop.Views;
using ThemeService = OpenTodoDesktop.Services.ThemeService;

namespace OpenTodoDesktop;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        
        var configService = GetService<ConfigureService>();
        configService.LoadAsync().GetAwaiter().GetResult();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = GetService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(sp => new ConfigureService(null));
        services.AddSingleton<LocalizationService>();
        services.AddSingleton<ThemeService>();
        
        // Windows
        services.AddTransient<MainWindowViewModel>(); 
        return;
    }

    public static T GetService<T>() where T : class
    {
        var result = ServiceProvider?.GetService<T>();
        if (result == null)
        {
            throw new InvalidOperationException($"Service of type {typeof(T)} could not be found.");
        }
        return result;
    }
}