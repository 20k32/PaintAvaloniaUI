using Avalonia.Controls;
using Paint_AvaloniaUI.ViewModels;
using Paint_AvaloniaUI.ViewModels.ControlViewModels;
using Paint_AvaloniaUI.Views.ControlViews;

namespace Paint_AvaloniaUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel()
        {
            PaintCanvasVM = new PaintCanvasViewModel()
        };
    }
}
