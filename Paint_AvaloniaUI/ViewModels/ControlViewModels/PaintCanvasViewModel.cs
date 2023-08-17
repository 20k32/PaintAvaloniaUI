using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using Paint_AvaloniaUI.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Paint_AvaloniaUI.ViewModels.ControlViewModels
{
    public partial class PaintCanvasViewModel : ViewModelBase
    {
        public ObservableCollection<Shape> Shapes { get; set; }

        internal PaintModelBase Paint = null!;

        private Action Notification = null!;

        internal PaintCanvasViewModel(PaintModelBase paint, Action notifyCanExecuteChanged = null!)
        {
            Shapes = new();
            Paint = paint;
            Notification = notifyCanExecuteChanged;
        }

        public void Undo()
        {
            Paint.Undo(Shapes);
            Notification?.Invoke();
        }
            

        public bool CanUndo() =>
            Shapes.Count != 0 && Paint.CanUndo(Shapes);

        public void ClearCanvas()
        {
            Paint.ClearCanvas(Shapes);
            Notification?.Invoke();
        }

        public bool CanClearCanvas() =>
            Paint.CanClearCanvas(Shapes);

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

            Notification?.Invoke();
        }

        [RelayCommand]
        public void CanvasPointerMoved(PointerEventArgs e)
        {
            Paint.OnPointerMoved(e);

            if (Paint.TemporaryResultShape != null)
            {
                Shapes.Add(Paint.TemporaryResultShape);
            }
        }

        #endregion
    }
}
