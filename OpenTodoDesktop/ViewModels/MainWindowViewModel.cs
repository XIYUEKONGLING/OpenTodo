using OpenTodoDesktop.Localization;
using OpenTodoDesktop.Services;

namespace OpenTodoDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public LocalizationService Localization { get; }
    public ConfigureService ConfigService { get; }
    
    public MainWindowViewModel()
    {
        ConfigService = new ConfigureService(null);
        Localization = new LocalizationService(ConfigService);
    }
    
    public MainWindowViewModel(LocalizationService localizationService, ConfigureService configService)
    {
        Localization = localizationService;
        ConfigService = configService;
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}