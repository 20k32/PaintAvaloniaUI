﻿using Avalonia;
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
        private const int MIN_LINE_RENDER_DISTANCE = 10;

        private static LinkedList<StubPolyline> TempPolyLines;
        private static LinkedList<StubLine> TempLines;
        private static LinkedList<Point> PointsForPolyline;

        private bool IsDrawing = false;

        public static IBrush Brush = new SolidColorBrush(Colors.Black);
        public static double StrokeThickness = 4;

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

            PreviousLocation = e.GetPositionRelative();
        }

        public void OnMouseReleased(PointerReleasedEventArgs e, ObservableCollection<Shape> shapes)
        {
            TempLines.Clear();

            var polyLine = StubPolyline
                .GetStubPolyline(PointsForPolyline.ToArray(), Brush, StrokeThickness);

            PointsForPolyline.Clear();

            TempPolyLines.AddLast(polyLine);

            IsDrawing = false;

            ClearStubLines(shapes);
            AddStubPolylines(shapes);
        }

        public Line OnMouseMove(PointerEventArgs e)
        {
            var currentLocation = e.GetPositionRelative();

            if (!IsDrawing
                || CalculateDistance(PreviousLocation, currentLocation) < MIN_LINE_RENDER_DISTANCE)
            {
                return null!;
            }

            var line = StubLine.GetStubLine(
                Brush,
                PreviousLocation,
                currentLocation,
                StrokeThickness);

            PointsForPolyline.AddLast(PreviousLocation);
            PointsForPolyline.AddLast(currentLocation);
            PreviousLocation = currentLocation;

            TempLines.AddLast(line);

            return line;
        }

        //this optimization needed to prevent memory leak
        //because of creating practically the same points and stublines

        private double CalculateDistance(Point a, Point b) =>
            Math.Sqrt(Math.Pow((b.X - a.X), 2) +
                Math.Pow((b.Y - a.Y), 2));

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