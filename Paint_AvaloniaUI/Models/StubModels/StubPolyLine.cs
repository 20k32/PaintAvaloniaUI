using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System.Collections.Generic;

namespace Paint_AvaloniaUI.Models.StubModels
{
    internal class StubPolyline : Polyline
    {
        public static StubPolyline GetStubPolyline(IList<Point> points, IBrush stroke, double thickness) =>
            new StubPolyline()
            {
                Stroke = stroke,
                Points = points,
                StrokeThickness = thickness
            };
    }
}
