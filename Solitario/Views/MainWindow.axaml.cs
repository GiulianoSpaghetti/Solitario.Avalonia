using Avalonia.Controls;

namespace Solitario.Views;

public partial class MainWindow : Window
{

    public static MainWindow Instance;
    public MainWindow()
    {
        Instance = this;
        InitializeComponent();
    }
}
