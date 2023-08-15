using Avalonia.Controls;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.ViewModels;

internal partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public PaintCanvasViewModel PaintCanvasVM { get; set; } = null!;
}
