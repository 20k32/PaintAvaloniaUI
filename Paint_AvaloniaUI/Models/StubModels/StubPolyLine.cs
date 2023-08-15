using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.Models.StubModels
{
    internal class StubPolyline : Polyline
    {
        public static StubPolyline GetStubPolyline(IBrush stroke, IList<Point> points, double thickness) =>
            new StubPolyline()
            {
                Stroke = stroke,
                Points = points,
                StrokeThickness = thickness
            };
    }
}
