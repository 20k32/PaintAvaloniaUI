using Avalonia.Controls;
using Paint_AvaloniaUI.ViewModels;

namespace Paint_AvaloniaUI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}
