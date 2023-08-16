using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using Paint_AvaloniaUI.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Paint_AvaloniaUI.ViewModels.ControlViewModels
{
    // attached to PaintCanvasView.axaml by viewlocator

    public partial class PaintCanvasViewModel : ViewModelBase
    {
        public ObservableCollection<Shape> Shapes { get; set; }

        internal PaintModelBase Paint = null!;

        public PaintCanvasViewModel()
        {
            Shapes = new();
        }

        public void ClearCanvas() =>
             Paint.ClearCanvas(Shapes);

        #region Commands

        [RelayCommand]
        public void CanvasPointerPressed(PointerPressedEventArgs e)
        {
            Paint.OnPointerPressed(e);
        }

        [RelayCommand]
        public void CanvasPointerReleased(PointerReleasedEventArgs e)
        {
            Paint.OnPointerReleased(e);
            Paint.ClearStubObjects(Shapes);
            Paint.AddRegularObjects(Shapes);
        }

        [RelayCommand]
        public void CanvasPointerMoved(PointerEventArgs e)
        {
            Paint.OnPointerMoved(e);

            if(Paint.TemporaryResultShape != null)
            {
                Shapes.Add(Paint.TemporaryResultShape);
            }
        }

        #endregion
    }
}
