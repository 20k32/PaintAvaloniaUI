using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.Collections.ObjectModel;

namespace Paint_AvaloniaUI.Models
{
    internal abstract class PaintModelBase
    {
        protected abstract int MinRenderDistance { get; }

        protected static bool IsDrawing;
        
        public static SolidColorBrush DrawingColor = null!;
        public static double DrawingThickness;
        public static SolidColorBrush CurrentCanvasBackground = null!;
        public static SolidColorBrush BrushFillColor = null!;

        public virtual void OnPointerPressed(PointerPressedEventArgs e) { }
        public abstract void OnPointerReleased(PointerReleasedEventArgs e);
        public abstract void Undo(ObservableCollection<Shape> shapes);
        public abstract bool CanUndo(ObservableCollection<Shape> shapes);
        public virtual void OnPointerMoved(PointerEventArgs e) { }
        public virtual void ClearStubObjects(ObservableCollection<Shape> shapes) { }
        public virtual void AddRegularObjects(ObservableCollection<Shape> shapes) { }
        public virtual void ClearCanvas(ObservableCollection<Shape> shapes) { }
        public virtual bool CanClearCanvas(ObservableCollection<Shape> shapes) =>
            shapes.Count != 0;

        public Shape TemporaryResultShape = null!;

        //this optimization needed to prevent memory leak
        //because of creating practically the same points and stublines
        protected double CalculateDistance(Point a, Point b) =>
            Math.Sqrt(Math.Pow((b.X - a.X), 2) +
                Math.Pow((b.Y - a.Y), 2));
    }
}
