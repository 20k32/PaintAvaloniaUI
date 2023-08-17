using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.OpenGL.Egl;
using Avalonia.OpenGL.Surfaces;
using Paint_AvaloniaUI.Models.Extensions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.Models
{
    internal class BrushFillModel : PaintModelBase
    {
        private HashSet<Point> CanvasShapesPoints = null!;
        private Shape TemporaryResultPolygon;
        private ObservableCollection<Shape> Shapes;

        public BrushFillModel(ObservableCollection<Shape> shapes)
        {
            CanvasShapesPoints = new();
            Shapes = shapes;
        }

        protected override int MinRenderDistance => 10;

        public override void AddRegularObjects(ObservableCollection<Shape> shapes)
        {
            if(TemporaryResultPolygon != null 
               && !shapes.Contains(TemporaryResultPolygon))
            {
                shapes.Add(TemporaryResultPolygon);
            }
        }

        public override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            AnalyzeCanvas(Shapes);

            if (CanvasShapesPoints.Count == 0) 
            {
                return;
            }

            var startPoint = e.GetPositionRelative();

            var shape = GetShapeRelativeTo(startPoint);
            
            if(shape != null)
            {
                var polygon = new Polygon()
                {
                    Points = ((Polyline)shape).Points,
                    Fill = BrushFillColor,
                };

                TemporaryResultPolygon = polygon;
            }
        }

        public void AnalyzeCanvas(ObservableCollection<Shape> Shapes)
        {
            foreach (var shape in Shapes)
            {
                switch (shape)
                {
                    case Polyline polyline:
                        AddPointSetToHashSet(polyline.Points);
                        break;
                }
            }
        }

        private void AddPointSetToHashSet(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                CanvasShapesPoints.Add(point);
            }
        }

        private Shape GetShapeRelativeTo(Point start)
        {
            HashSet<Point> visitedPoints = new();

            while(visitedPoints.Count != CanvasShapesPoints.Count)
            {
                var minPoint = CanvasShapesPoints.First();
                double minDistanceToElement = CalculateDistance(start, minPoint);

                foreach (var point in CanvasShapesPoints.Skip(1))
                {
                    var tempResult = CalculateDistance(start, point);

                    if (tempResult < minDistanceToElement
                        && !visitedPoints.Contains(point))
                    {
                        minDistanceToElement = tempResult;
                        minPoint = point;
                    }
                }

                visitedPoints.Add(minPoint);
                
                foreach (var shape in Shapes)
                {
                    switch (shape)
                    {
                        case Polyline polyline:
                            foreach (var point in polyline.Points)
                            {
                                if (point.NearlyEquals(minPoint))
                                {
                                    return polyline;
                                }
                            } 
                            break;
                    }
                }
            }
            
            return null!;
        }

        public override void ClearCanvas(ObservableCollection<Shape> shapes)
        {
            CanvasShapesPoints.Clear();
            TemporaryResultPolygon = null!;
            shapes.Clear();
        }

    }
}
