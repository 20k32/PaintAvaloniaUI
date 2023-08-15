using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
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

    public static class PointerPressedExtensions
    {
        public static Point GetCoordsRelativeToCanvas(this PointerEventArgs e) =>
            e.GetPosition((Canvas)e.Source!);
    }

    internal partial class PaintCanvasViewModel : ViewModelBase
    {
        public ObservableCollection<Shape> Shapes { get; private set; } = null!;
        public Canvas RelativeCanvas { get; set; } = null!;

        public PaintCanvasViewModel()
        {
            Shapes = new();
            RelativeCanvas = new();
        }

        [RelayCommand]
        public void CanvasPointerPressed(PointerPressedEventArgs e)
        {
            
        }

        [RelayCommand]
        public void CanvasPointerReleased(PointerReleasedEventArgs e)
        {
            
        }

        [RelayCommand]
        public void CanvasPointerMoved(PointerEventArgs e)
        {
            Debug.WriteLine(e.GetCoordsRelativeToCanvas());
        }
    }
}
