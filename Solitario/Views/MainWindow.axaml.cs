using Avalonia.Controls;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;
using System.Runtime.InteropServices;
using System;

namespace Solitario.Views;

public partial class MainWindow : Window
{

    public static MainWindow Instance;
    public static INotificationManager notification;

    private static INotificationManager CreateManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return new FreeDesktopNotificationManager();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return new DesktopNotifications.Windows.WindowsNotificationManager(null);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return new DesktopNotifications.Apple.AppleNotificationManager();

        throw new PlatformNotSupportedException();
    }
    public MainWindow()
    {
        Instance = this;
        InitializeComponent();
        notification = CreateManager();
        notification.Initialize();
    }
}
