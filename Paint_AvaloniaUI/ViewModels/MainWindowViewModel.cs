using Avalonia.Controls;
using Avalonia.Diagnostics;
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

    internal PaintCanvasViewModel PaintCanvasVM => paintCanvasViewModel;

    public MainWindowViewModel()
    {
        paintCanvasViewModel = new PaintCanvasViewModel(UserDrawingStyle, CanExecuteCommandsUpdater);

        DrawingStyles = new PaintModelBase[3]
        {
            new HandDrawingModel(),
            new BrushFillModel(PaintCanvasVM.Shapes),
            new StraightLineModel(PaintCanvasVM.Shapes),
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

    private void CanExecuteCommandsUpdater()
    {
        ClearDrawingCommand.NotifyCanExecuteChanged();
        EraseDrawingCommand.NotifyCanExecuteChanged();
        UndoLastDrawingCommand.NotifyCanExecuteChanged();
    }

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

    private Color brushDrawingColorSource = Colors.Black;

    public Color BrushDrawingColorSource
    {
        get => brushDrawingColorSource;
        set
        {
            brushDrawingColorSource = value;
            BrushDrawingColor.Color = brushDrawingColorSource;
        }
    }

    private SolidColorBrush brushDrawing = new SolidColorBrush(Colors.Black);

    public SolidColorBrush BrushDrawingColor
    {
        get => brushDrawing;
        set => SetProperty(ref brushDrawing, value);
    }

    #endregion

    #region BrushFill
    private Color brushFillColorSource = Colors.Black;

    public Color BrushFillColorSource
    {
        get => brushFillColorSource;
        set
        {
            brushFillColorSource = value;
            BrushFillColor = new SolidColorBrush(brushFillColorSource);
        }
    }

    private SolidColorBrush brushFillColor = new SolidColorBrush(Colors.Black);
    public SolidColorBrush BrushFillColor
    {
        get => brushFillColor;
        set => SetProperty(ref brushFillColor, value);
    }

    #endregion

    #region Background

    private Color backgroundColorSource = Colors.AntiqueWhite;

    public Color BackgroundColorSource
    {
        get => backgroundColorSource;
        set
        {
            SetProperty(ref backgroundColorSource, value);
            Background.Color = backgroundColorSource;
        }
    }

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

    [RelayCommand]
    private void StraightLineStyle()
    {
        UserDrawingStyle = DrawingStyles[2];
        CurrentCommand = BrushFillStyleCommand;
        CanExecuteCommandsUpdater();
    }

    #endregion

    #region ChangeColorCommands

    [RelayCommand]
    private void ToogleDrawingColorPicker()
    {
        var colorPicker = new ColorPicker();
    }

    #endregion
    
    #region UndoCommand

    [RelayCommand(CanExecute = nameof(CanUndoLastDrawing))]
    private void UndoLastDrawing() =>
        PaintCanvasVM.Undo();

    private bool CanUndoLastDrawing() =>
        PaintCanvasVM.CanUndo();

    #endregion
}
