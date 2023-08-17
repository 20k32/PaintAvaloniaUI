using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private PaintCanvasViewModel paintCanvasViewModel = null!;
    private IRelayCommand CurrentCommand = null!;
    private PaintModelBase[] DrawingStyles = null!;

    public SolidColorBrush BrushFillColor => new SolidColorBrush(Colors.Black);

    internal PaintCanvasViewModel PaintCanvasVM => paintCanvasViewModel;

    public MainWindowViewModel()
    {
        paintCanvasViewModel = new PaintCanvasViewModel(UserDrawingStyle, CanExecuteCommandsUpdater);

        DrawingStyles = new PaintModelBase[2]
        {
            new HandDrawingModel(),
            new BrushFillModel(PaintCanvasVM.Shapes)
        };
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

    [RelayCommand(CanExecute = nameof(CanExecClearCanvas))]
    private void ClearDrawing()
    {
        var model = PaintCanvasVM.Paint;

        foreach (var item in DrawingStyles)
        {
            PaintCanvasVM.Paint = item;
            PaintCanvasVM.ClearCanvas();
        }
        PaintCanvasVM.Paint = model;
    }

    private bool CanExecDrawing() =>
        PaintCanvasVM.CanClearCanvas()
        && CurrentCommand != BrushFillStyleCommand;

    private bool CanExecClearCanvas() =>
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
        UserDrawingStyle = DrawingStyles[0];
        CurrentCommand = HandDrawingStyleCommand;
        CanExecuteCommandsUpdater();
    }

    [RelayCommand]
    private void BrushFillStyle()
    {
        UserDrawingStyle = DrawingStyles[1];
        CurrentCommand = BrushFillStyleCommand;
        CanExecuteCommandsUpdater();
    }

    #endregion
}
