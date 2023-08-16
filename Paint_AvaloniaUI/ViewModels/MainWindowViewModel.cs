using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.Input;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private PaintCanvasViewModel paintCanvasViewModel = null!;

    internal PaintCanvasViewModel PaintCanvasVM => paintCanvasViewModel;

    public MainWindowViewModel()
    {
        paintCanvasViewModel = new PaintCanvasViewModel()
        {
            Paint = UserDrawingStyle
        };
    }

    internal PaintModelBase UserDrawingStyle => new HandDrawingModel();

    #region Thickness

    private double brushDrawingThickness = 15;

    public double BrushDrawingThickness
    {
        get => brushDrawingThickness;
        set => SetProperty(ref brushDrawingThickness, value);
    }

    #endregion

    #region Color

    private SolidColorBrush brushDrawing = new SolidColorBrush(Colors.Green);

    public SolidColorBrush BrushDrawingColor
    {
        get => brushDrawing;
        set => SetProperty(ref brushDrawing, value);
    }

    #endregion

    #region Background

    private SolidColorBrush background = new SolidColorBrush(Colors.AntiqueWhite);

    public SolidColorBrush Background
    {
        get => background;
        set => SetProperty(ref background, value);
    }

    #endregion


    #region Commands

    [RelayCommand]
    private void EraseDrawing()
    {
        if (BrushDrawingColor.Color != Colors.AntiqueWhite)
        {
            BrushDrawingColor.Color = Colors.AntiqueWhite;
        }
        else
        {
            BrushDrawingColor.Color = Colors.Green;
        }

        OnPropertyChanged(nameof(BrushDrawingColor));
    }

    [RelayCommand]
    public void ClearDrawing() =>
        PaintCanvasVM.ClearCanvas();

    #endregion
}
