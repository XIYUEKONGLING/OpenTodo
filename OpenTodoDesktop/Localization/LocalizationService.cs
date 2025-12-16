using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenTodoDesktop.Localization;

public class LocalizationService : ObservableObject
{
    public const string TranslationsLocation = "avares://OpenTodoDesktop/Assets/I18n/";
    
    private const string DefaultLanguage = "en-US";
    private readonly Dictionary<string, Dictionary<string, string>> _translations = new();
    
    private string _currentLanguage = DefaultLanguage;

    public LocalizationService()
    {
        LoadTranslations();
    }

    /// <summary>
    /// Gets the current language code (e.g., "en-US").
    /// </summary>
    public string CurrentLanguage
    {
        get => _currentLanguage;
        private set => SetProperty(ref _currentLanguage, value);
    }

    /// <summary>
    /// Indexer for XAML binding. Usage: {Binding Localization[program.name]}
    /// </summary>
    public string this[string key] => GetString(key);

    /// <summary>
    /// Sets the current language and notifies listeners to update bindings.
    /// </summary>
    /// <param name="languageCode">The language code (e.g., "zh-CN").</param>
    public void SetLanguage(string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode) || !_translations.ContainsKey(languageCode))
        {
            // Fallback or ignore invalid language codes
            return;
        }

        CurrentLanguage = languageCode;
        
        OnPropertyChanged(string.Empty); 
    }

    /// <summary>
    /// Retrieves a localized string. Falls back to DefaultLanguage or the Key itself if not found.
    /// </summary>
    public string GetString(string key)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;

        // 1. Try current language
        if (_translations.TryGetValue(_currentLanguage, out var currentDict) && 
            currentDict.TryGetValue(key, out var value))
        {
            return value;
        }

        // 2. Try default language (Fallback)
        if (_currentLanguage != DefaultLanguage && 
            _translations.TryGetValue(DefaultLanguage, out var defaultDict) && 
            defaultDict.TryGetValue(key, out var defaultValue))
        {
            return defaultValue;
        }

        // 3. Return key as last resort
        return key;
    }

    private void LoadTranslations()
    {
        _translations.Clear();
        
        // Ensure the dictionary structure exists for the default language at minimum
        if (!_translations.ContainsKey(DefaultLanguage))
        {
            _translations[DefaultLanguage] = new Dictionary<string, string>();
        }

        try
        {
            var localesUri = new Uri(TranslationsLocation);
            var assets = AssetLoader.GetAssets(localesUri, null);

            foreach (var uri in assets)
            {
                // Parse language code from filename (e.g., "en-US.json" -> "en-US")
                var fileName = Path.GetFileNameWithoutExtension(uri.AbsolutePath);

                if (string.IsNullOrEmpty(fileName))
                    continue;

                using var stream = AssetLoader.Open(uri);
                using var reader = new StreamReader(stream);
                var jsonContent = reader.ReadToEnd();

                var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
                if (translations != null)
                {
                    _translations[fileName] = translations;
                }
            }
        }
        catch (Exception)
        {
            // Ignored
        }
        finally
        {
            if (!_translations.ContainsKey(DefaultLanguage))
            {
                _translations.Add(DefaultLanguage, new Dictionary<string, string>());
            }
        }
    }
}
