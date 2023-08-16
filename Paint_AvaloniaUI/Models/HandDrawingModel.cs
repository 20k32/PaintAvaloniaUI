using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Paint_AvaloniaUI.Models.Extensions;
using Paint_AvaloniaUI.Models.StubModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.Models
{
    internal class HandDrawingModel : PaintModelBase
    {
        protected override int MinRenderDistance => 10;

        private static LinkedList<StubPolyline> TempPolyLines;
        private static LinkedList<StubLine> TempLines;
        private static LinkedList<Point> PointsForPolyline;
        
        private Point PreviousLocation;

        static HandDrawingModel()
        {
            TempPolyLines = new();
            TempLines = new();
            PointsForPolyline = new();
        }

        public HandDrawingModel()
        { }

        public override void OnPointerPressed(PointerPressedEventArgs e)
        {
            IsDrawing = true;

            PreviousLocation = e.GetPositionRelative();
        }

        public override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            var polyLine = StubPolyline
                .GetStubPolyline(PointsForPolyline.ToArray(), DrawingColor, DrawingThickness);

            PointsForPolyline.Clear();

            TempPolyLines.AddLast(polyLine);

            IsDrawing = false;
        }

        public override void OnPointerMoved(PointerEventArgs e)
        {
            var currentLocation = e.GetPositionRelative();

            if (!IsDrawing
                || CalculateDistance(PreviousLocation, currentLocation) < MinRenderDistance)
            {
                TemporaryResultShape = null!;

                return;
            }

            var line = StubLine.GetStubLine(
                DrawingColor,
                PreviousLocation,
                currentLocation,
                DrawingThickness);

            PointsForPolyline.AddLast(PreviousLocation);
            PointsForPolyline.AddLast(currentLocation);
            PreviousLocation = currentLocation;

            TempLines.AddLast(line);

            TemporaryResultShape = line;
        }

        //this optimization needed to prevent memory leak
        //because of creating practically the same points and stublines

        private double CalculateDistance(Point a, Point b) =>
            Math.Sqrt(Math.Pow((b.X - a.X), 2) +
                Math.Pow((b.Y - a.Y), 2));

        public override void ClearStubObjects(ObservableCollection<Shape> shapes)
        {
            foreach (var item in TempLines)
            {
                if (shapes.Contains(item))
                {
                    shapes.Remove(item);
                }
            }

            TempLines.Clear();
        }

        public override void AddRegularObjects(ObservableCollection<Shape> shapes)
        {
            foreach (var item in TempPolyLines)
            {
                if (!shapes.Contains(item))
                {
                    shapes.Add(item);
                }
            }
        }

        public override void ClearCanvas(ObservableCollection<Shape> shapes)
        {
            shapes.Clear();
            TempPolyLines.Clear();
        }
    }
}
