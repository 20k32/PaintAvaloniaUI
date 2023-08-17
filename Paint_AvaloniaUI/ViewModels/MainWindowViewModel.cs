using Avalonia.Media;
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
        paintCanvasViewModel = new PaintCanvasViewModel(UserDrawingStyle, CanExecuteCommandsUpdater);
    }

    #region DrawingStyle

    private PaintModelBase userDrawingStyle = new HandDrawingModel();
    
    internal PaintModelBase UserDrawingStyle
    {
        get => userDrawingStyle;
        set => SetProperty(ref userDrawingStyle, value);
    }


    #endregion

    #region EraseButtonName

    private string eraseButtonName = "Erase";

    public string EraseButtonName
    {
        get => eraseButtonName;
        set => SetProperty(ref eraseButtonName, value);
    }

    #endregion

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

    #region DrawingCommands

    [RelayCommand(CanExecute = nameof(CanExecDrawing))]
    private void EraseDrawing()
    {
        if (BrushDrawingColor.Color != Colors.AntiqueWhite)
        {
            BrushDrawingColor.Color = Colors.AntiqueWhite;
            EraseButtonName = "Draw";
        }
        else
        {
            BrushDrawingColor.Color = Colors.Green;
            EraseButtonName = "Erase";
        }

        OnPropertyChanged(nameof(BrushDrawingColor));
    }

    [RelayCommand(CanExecute = nameof(CanExecDrawing))]
    private void ClearDrawing() =>
        PaintCanvasVM.ClearCanvas();

    private bool CanExecDrawing() =>
        PaintCanvasVM.CanClearCanvas();

    private void CanExecuteCommandsUpdater()
    {
        ClearDrawingCommand.NotifyCanExecuteChanged();
        EraseDrawingCommand.NotifyCanExecuteChanged();
    }

    #endregion

    #region DrawingStyleCommands

    [RelayCommand]
    private void HandDrawingStyle()
    {
        UserDrawingStyle = new HandDrawingModel();
    }

    [RelayCommand]
    private void BrushFillStyle()
    {
        UserDrawingStyle = new BrushFillModel(PaintCanvasVM.Shapes);
    }

    #endregion
}
