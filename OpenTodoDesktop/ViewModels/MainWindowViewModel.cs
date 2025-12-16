using OpenTodoDesktop.Localization;

namespace OpenTodoDesktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public LocalizationService Localization { get; }
    
    public MainWindowViewModel()
    {
        Localization = new LocalizationService();
    }
    
    public MainWindowViewModel(LocalizationService localizationService)
    {
        Localization = localizationService;
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}