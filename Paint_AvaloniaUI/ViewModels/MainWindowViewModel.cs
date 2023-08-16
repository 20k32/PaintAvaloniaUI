using Avalonia.Controls;
using Avalonia.Media;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    internal PaintCanvasViewModel PaintCanvasVM => new PaintCanvasViewModel()
    {
        //default value, mydrawingstyle property can change it
        Paint = MyDrawingStyle
    };

    internal PaintModelBase MyDrawingStyle => new HandDrawingModel();

    public double MyDrawingThickness => 20;

    public IBrush MyDrawingColor => new SolidColorBrush(Colors.Green);
}
