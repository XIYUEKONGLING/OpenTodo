using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using OpenTodoDesktop.Models;

namespace OpenTodoDesktop.Services;

public partial class ThemeService : ObservableObject
{
    [ObservableProperty] 
    private ApplicationTheme _currentTheme = ApplicationTheme.System;
    
    [ObservableProperty] 
    private WindowBackdropType _backdropType = WindowBackdropType.Blur;
    
    [ObservableProperty] 
    private double _windowOpacity = 1.0;
    
    partial void OnCurrentThemeChanged(ApplicationTheme value)
    {
        if (Application.Current is null) return;
        Application.Current.RequestedThemeVariant = value switch
        {
            ApplicationTheme.Light => ThemeVariant.Light,
            ApplicationTheme.Dark => ThemeVariant.Dark,
            _ => ThemeVariant.Default
        };
    }
    
    partial void OnBackdropTypeChanged(WindowBackdropType value)
    {
        return;   
    }
}