using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenTodoDesktop.Models;

public partial class ApplicationData : ObservableObject
{
    [ObservableProperty]
    private DateTime _lastRunTime = DateTime.UtcNow;
}