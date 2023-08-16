using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.Views.ControlViews
{
    public partial class PaintCanvasView : UserControl
    {
        private PaintCanvasViewModel ViewModel = null!;

        public PaintCanvasView()
        {
            InitializeComponent();
        }

        #region DrawingColor

        internal static readonly DirectProperty<PaintCanvasView, SolidColorBrush> DrawingColorProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, SolidColorBrush>(
                nameof(DrawingColor),
                getter => getter.DrawingColor,
                (setter, value) => setter.DrawingColor = value);

        public SolidColorBrush DrawingColor
        {
            get => PaintModelBase.DrawingColor;
            set => SetAndRaise(DrawingColorProperty, ref PaintModelBase.DrawingColor, value);
        }

        #endregion

        #region DrawinghThickness

        internal static readonly DirectProperty<PaintCanvasView, double> DrawingThicknessProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, double>(
                nameof(DrawingThickness),
                getter => getter.DrawingThickness,
                (setter, value) => setter.DrawingThickness = value);

        public double DrawingThickness
        {
            get => PaintModelBase.DrawingThickness;
            set => SetAndRaise(DrawingThicknessProperty, ref PaintModelBase.DrawingThickness, value);
        }

        #endregion

        #region DrawingStyle

        internal static readonly DirectProperty<PaintCanvasView, PaintModelBase> DrawingStyleProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, PaintModelBase>(
                nameof(DrawingStyle),
                getter => getter.DrawingStyle,
                (setter, value) => setter.DrawingStyle = value);

        internal PaintModelBase DrawingStyle
        {
            get => ViewModel.Paint;
            set
            {
                if(ViewModel is not null)
                {
                    SetAndRaise(DrawingStyleProperty, ref ViewModel.Paint, value);
                }
            }
        }

        #endregion

        #region Background

        internal static readonly DirectProperty<PaintCanvasView, SolidColorBrush> CanvasBackgroundProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, SolidColorBrush>(
                nameof(CanvasBackground),
                getter => getter.CanvasBackground,
                (setter, value) => setter.CanvasBackground = value);

        public SolidColorBrush CanvasBackground
        {
            get => PaintModelBase.CurrentCanvasBackground;
            set
            {
                SetAndRaise(CanvasBackgroundProperty, ref PaintModelBase.CurrentCanvasBackground, value);
            }
        }

        #endregion
    }
}
