using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using Paint_AvaloniaUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.ViewModels.ControlViewModels
{
    internal partial class PaintCanvasViewModel : ViewModelBase
    {
        public ObservableCollection<Shape> Shapes { get; set; }

        private PaintCanvasModel Paint;

        public PaintCanvasViewModel()
        {
            Paint = new();
            Shapes = new();
        }

        [RelayCommand]
        public void CanvasPointerPressed(PointerPressedEventArgs e)
        {
            Paint.OnMousePressed(e);
        }

        [RelayCommand]
        public void CanvasPointerReleased(PointerReleasedEventArgs e)
        {
            Paint.OnMouseReleased(e, Shapes);
        }

        [RelayCommand]
        public void CanvasPointerMoved(PointerEventArgs e)
        {
            var line = Paint.OnMouseMove(e);

            if(line  != null)
            {
                Shapes.Add(line);
            }
        }
    }
}
