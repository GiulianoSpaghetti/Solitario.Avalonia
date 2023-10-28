using Avalonia.Controls;
using DesktopNotifications;

namespace Solitario.Views;

public partial class MainWindow : Window
{

    public static MainWindow Instance;
    public static INotificationManager notification;

    public MainWindow()
    {
        Instance = this;
        InitializeComponent();
    }
}
