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
    internal class PaintCanvasModel
    {

        private bool IsDrawing = false;

        private static LinkedList<StubPolyline> TempPolyLines;
        private static LinkedList<StubLine> TempLines;
        private static LinkedList<Point> PointsForPolyline;

        private Point PreviousLocation;

        static PaintCanvasModel()
        {
            TempPolyLines = new();
            TempLines = new();
            PointsForPolyline = new();
        }

        public PaintCanvasModel()
        { }

        public void OnMousePressed(PointerPressedEventArgs e)
        {
            IsDrawing = true;

            PointsForPolyline.Clear();

            PreviousLocation = e.GetPositionRelative();
        }

        public void OnMouseReleased(PointerReleasedEventArgs e, ObservableCollection<Shape> shapes)
        {
            TempLines.Clear();

            var polyLine = StubPolyline.GetStubPolyline(
                 new SolidColorBrush(Color.FromRgb(55, 155, 255)),
                 PointsForPolyline.ToArray(), 4);

            TempPolyLines.AddLast(polyLine);

            IsDrawing = false;

            ClearStubLines(shapes);
            AddStubPolylines(shapes);
        }

        public Line OnMouseMove(PointerEventArgs e)
        {
            if (!IsDrawing)
            {
                return null!;
            }

            var currentLocation = e.GetPositionRelative();
            
            var line = StubLine.GetStubLine(
                new SolidColorBrush(Color.FromRgb(55, 155, 255)),
                PreviousLocation,
                currentLocation,
                4);
               
            TempLines.AddLast(line);

            PointsForPolyline.AddLast(PreviousLocation);
            PointsForPolyline.AddLast(currentLocation);

            PreviousLocation = currentLocation;

            return line;
        }

        private void ClearStubLines(ObservableCollection<Shape> shapes)
        {
            foreach (var item in TempLines)
            {
                if (shapes.Contains(item))
                {
                    shapes.Remove(item);
                }
            }
        }

        private void AddStubPolylines(ObservableCollection<Shape> shapes)
        {
            foreach (var item in TempPolyLines)
            {
                if (!shapes.Contains(item))
                {
                    shapes.Add(item);
                }
            }
        }
    }
}
