using Avalonia.Controls;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;
using System.Runtime.InteropServices;
using System;
using System.Globalization;

namespace Solitario.Views;

public partial class MainWindow : Window
{
    public static ResourceDictionary d;
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
        InitializeComponent();
        d = this.FindResource(CultureInfo.CurrentCulture.TwoLetterISOLanguageName) as ResourceDictionary;
        if (d == null)
            d = this.FindResource("it") as ResourceDictionary;
        MainView.Instance.Inizializza();
        Instance = this;
        notification = CreateManager();
        notification.Initialize();
    }
}
