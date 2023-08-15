using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Paint_AvaloniaUI.Models.StubModels
{
    internal class StubLine : Line
    {
        public static StubLine GetStubLine(IBrush stroke, Point start, Point end, double thickness) =>
            new StubLine()
            {
                Stroke = stroke,
                StartPoint = start,
                EndPoint = end,
                StrokeThickness = thickness
            };
    }
}
