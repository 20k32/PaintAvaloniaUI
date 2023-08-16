using Avalonia.Controls;
using Avalonia.Media;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    internal PaintCanvasViewModel PaintCanvasVM => new PaintCanvasViewModel()
    {
        //default value, mydrawingstyle property can change it in future
        Paint = MyDrawingStyle
    };

    internal PaintModelBase MyDrawingStyle => new HandDrawingModel();

    private double myDrawingThickness = 10;

    public double MyDrawingThickness
    {
        get => myDrawingThickness;
        set=> SetProperty(ref myDrawingThickness, value);
    }

    public IBrush MyDrawingColor => new SolidColorBrush(Colors.Green);
}
