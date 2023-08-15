using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Paint_AvaloniaUI.Models;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;

namespace Paint_AvaloniaUI.Views.ControlViews
{
    public partial class PaintCanvasView : UserControl
    {
        public PaintCanvasView()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static DirectProperty<PaintCanvasView, IBrush> DrawingColorProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, IBrush>(
                nameof(DrawingColor),
                getter => getter.DrawingColor,
                (setter, value) => setter.DrawingColor = value);

        public IBrush DrawingColor
        {
            get => PaintCanvasModel.Brush;
            set => SetAndRaise(DrawingColorProperty, ref PaintCanvasModel.Brush, value);
        }

        public static DirectProperty<PaintCanvasView, double> DrawingThicknessProperty =
            AvaloniaProperty.RegisterDirect<PaintCanvasView, double>(
                nameof(DrawingThickness),
                getter => getter.DrawingThickness,
                (setter, value) => setter.DrawingThickness = value);

        public double DrawingThickness
        {
            get => PaintCanvasModel.StrokeThickness;
            set => SetAndRaise(DrawingThicknessProperty, ref PaintCanvasModel.StrokeThickness, value);
        }

        #endregion
    }
}
