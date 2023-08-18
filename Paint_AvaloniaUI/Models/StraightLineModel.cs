using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Paint_AvaloniaUI.Models.Extensions;
using Paint_AvaloniaUI.Models.StubModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.Models
{
    internal class StraightLineModel : PaintModelBase
    {
        protected override int MinRenderDistance => 10;

        private static LinkedList<StubLine> ResultLines = null!;
        private static LinkedList<StubLine> TempLines = null!;

        private ObservableCollection<Shape> Shapes = null!;
        private Point PreviousLocation;

        static StraightLineModel()
        {
            ResultLines = new();
            TempLines = new();
        }

        public StraightLineModel(ObservableCollection<Shape> shapes)
        {
            Shapes = shapes;
        }

        public override void OnPointerPressed(PointerPressedEventArgs e)
        {
            IsDrawing = true;

            PreviousLocation = e.GetPositionRelative();
        }

        public override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            var line = StubLine
                .GetStubLine(DrawingColor, PreviousLocation, e.GetPositionRelative(), DrawingThickness);

            ResultLines.AddLast(line);

            IsDrawing = false;
        }

        public override void OnPointerMoved(PointerEventArgs e)
        {
            var currentLocation = e.GetPositionRelative();

            if (!IsDrawing)
            {
                TemporaryResultShape = null!;
                return;
            }

            if(TemporaryResultShape != null)
            {
                Shapes.Remove(TemporaryResultShape);

                var tempLine = (StubLine)TemporaryResultShape!;

                if (CalculateDistance(tempLine.EndPoint, currentLocation) < MinRenderDistance)
                {
                    return;
                }
            }
            

            var line = StubLine.GetStubLine(
               DrawingColor,
               PreviousLocation,
               currentLocation,
               DrawingThickness);

            TempLines.AddLast(line);
            TemporaryResultShape = line;
        }

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
            foreach (var item in ResultLines)
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
            ResultLines.Clear();
            TempLines.Clear();
            TemporaryResultShape = null!;
        }

        public override void Undo(ObservableCollection<Shape> shapes)
        {
            var lastPolyline = shapes.Last(elem => elem.GetType() == typeof(StubLine));

            if (lastPolyline != null)
            {
                shapes.Remove(lastPolyline);
                ResultLines.RemoveLast();
            }
        }
        public override bool CanUndo(ObservableCollection<Shape> shapes) =>
            ResultLines.Count != 0
            && shapes
                   .FirstOrDefault(elem => elem.GetType() == typeof(StubLine)) != null;
    }
}
