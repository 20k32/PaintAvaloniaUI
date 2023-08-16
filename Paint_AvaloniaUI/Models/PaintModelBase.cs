using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using System.Collections.ObjectModel;

namespace Paint_AvaloniaUI.Models
{
    internal abstract class PaintModelBase
    {
        protected abstract int MinRenderDistance { get; }

        protected static bool IsDrawing;
        
        public static IBrush DrawingColor = null!;
        public static double DrawingThickness;

        public abstract void OnPointerPressed(PointerPressedEventArgs e);
        public abstract void OnPointerReleased(PointerReleasedEventArgs e);
        public abstract void OnPointerMoved(PointerEventArgs e);

        public abstract void ClearStubObjects(ObservableCollection<Shape> shapes);
        public abstract void AddRegularObjects(ObservableCollection<Shape> shapes);

        public Shape TemporaryResultShape = null!;
    }
}
