﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public override void Undo(ObservableCollection<Shape> shapes)
        {
            var lastPolyline = shapes.Last(elem => elem.GetType() == typeof(StubPolyline));

            if (lastPolyline != null)
            {
                shapes.Remove(lastPolyline);
                TempPolyLines.RemoveLast();
            }
        }
        public override bool CanUndo(ObservableCollection<Shape> shapes) => 
            TempPolyLines.Count != 0
            &&  shapes
                   .FirstOrDefault(elem => elem.GetType() == typeof(StubPolyline)) != null;
    }
}
