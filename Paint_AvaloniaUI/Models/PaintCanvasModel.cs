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

        private LinkedList<StubPolyline> TempPolyLines;
        private LinkedList<StubLine> TempLines;
        
        public List<Point> PointsForPolyline;

        private Point PreviousLocation;

        public PaintCanvasModel()
        {
            TempPolyLines = new();
            TempLines = new();
        }

        public void OnMousePressed(PointerPressedEventArgs e)
        {
            IsDrawing = true;
            
            PointsForPolyline = new();

            PreviousLocation = e.GetCoordsRelativeToCanvas();
        }

        public void OnMouseReleased(PointerReleasedEventArgs e, ObservableCollection<Shape> shapes)
        {
            TempLines.Clear();

            var polyLine = StubPolyline.GetStubPolyline(
                 new SolidColorBrush(Color.FromRgb(55, 155, 255)),
                 PointsForPolyline, 4);

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
            var currentLocation = e.GetCoordsRelativeToCanvas();
            
            var line = StubLine.GetStubLine(
                new SolidColorBrush(Color.FromRgb(55, 155, 255)),
                PreviousLocation,
                currentLocation,
                4);
               
            TempLines.AddLast(line);

            PointsForPolyline.Add(PreviousLocation);
            PointsForPolyline.Add(currentLocation);

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
