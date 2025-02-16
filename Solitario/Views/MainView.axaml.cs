using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
using org.altervista.numerone.framework;
using DesktopNotifications;
using DesktopNotifications.FreeDesktop;
using System.Runtime.InteropServices;

namespace Solitario.Views;

public partial class MainView : UserControl
{
    UInt16 i, j, k;
    private Carta[] vettore;
    private ulong mosse = 0;

    public MainView()
    {
        int a = 0, b = 0;
        InitializeComponent();
        vettore = new Carta[30];
        ElaboratoreCarteBriscola e = new ElaboratoreCarteBriscola(true, 9, 1, 9);
        Mazzo m = new Mazzo(e);
        org.altervista.numerone.framework.solitario.CartaHelper chs = new org.altervista.numerone.framework.solitario.CartaHelper();
        m.SetNome("Napoletano");
        Carta.Inizializza("", m, 10, chs, "","","","","","","","", "Solitario");

        for (UInt16 i = 0; i < 9; i++)
        {
            vettore[a * 10 + b] = Carta.GetCarta(m.GetCarta());
            a++;
            if (a > 2)
            {
                a = 0;
                b++;
            }
        }
        carta0.Source = vettore[0].GetImmagine();
        carta0.IsVisible = true;
        carta1.Source = vettore[1].GetImmagine();
        carta1.IsVisible = true;
        carta2.Source = vettore[2].GetImmagine();
        carta2.IsVisible = true;
        carta10.Source = vettore[10].GetImmagine();
        carta10.IsVisible = true;
        carta11.Source = vettore[11].GetImmagine();
        carta11.IsVisible = true;
        carta12.Source = vettore[12].GetImmagine();
        carta12.IsVisible = true;
        carta20.Source = vettore[20].GetImmagine();
        carta20.IsVisible = true;
        carta21.Source = vettore[21].GetImmagine();
        carta21.IsVisible = true;
        carta22.Source = vettore[22].GetImmagine();
        carta22.IsVisible = true;
        i = j = k = 2;
        ok.Content = MainWindow.Instance.FindResource("Ok");
        Prompt.Content = MainWindow.Instance.FindResource("SpostaCarta");
        Prompt1.Content = MainWindow.Instance.FindResource("SuRiga");
        info.Content = MainWindow.Instance.FindResource("Informazioni");
        greetingsClose.Content = MainWindow.Instance.FindResource("Annulla");
        greetingsOk.Content = MainWindow.Instance.FindResource("Ok");
        condividi.Content = MainWindow.Instance.FindResource("Condividi");
        InfoApp.Content = MainWindow.Instance.FindResource("Applicazione");
        lblinfo.Content = MainWindow.Instance.FindResource("info");
        lblimageinfo.Content = MainWindow.Instance.FindResource("imageinfo");
    }

    private void OnOk_Click(object sender, RoutedEventArgs e)
    {
        UInt16 inizio, fine, a = 0, b = 0;
        if (Inizio.Text == null || Inizio.Text == "")
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiInizio")} {MainWindow.Instance.FindResource("EVuota")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }

        if (Fine.Text == null || Fine.Text == "")
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiFine")} {MainWindow.Instance.FindResource("EVuota")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }

        try
        {
            inizio = UInt16.Parse(Inizio.Text);
        }
        catch (FormatException ex)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiInizio")} {MainWindow.Instance.FindResource("NonIntera")}"
            };

            MainWindow.notification.ShowNotification(not);
           return;
        }
        inizio--;
        try
        {
            fine = UInt16.Parse(Fine.Text);
        }
        catch (FormatException ex)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiFine")} {MainWindow.Instance.FindResource("NonIntera")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        fine--;
        if (inizio > 2)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiInizio")} {MainWindow.Instance.FindResource("NonNelRange")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        if (fine > 2)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiFine")} {MainWindow.Instance.FindResource("NonNelRange")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        if (inizio == fine)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LeRigheCoincidono")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        switch (inizio)
        {
            case 0: a = i; break;
            case 1: a = j; break;
            case 2: a = k; break;
        }
        switch (fine)
        {
            case 0: b = i; break;
            case 1: b = j; break;
            case 2: b = k; break;
        }
        Carta c;
        try
        {
            c = vettore[inizio * 10 + a];
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("LaRiga")} {MainWindow.Instance.FindResource("DiInizio")} {MainWindow.Instance.FindResource("EVuota")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        Carta c1 = null;
        try
        {
            c1 = vettore[fine * 10 + b];
        }
        catch (IndexOutOfRangeException ex) {; }
        if (c.CompareTo(c1) == -1)
        {
            Notification not = new Notification
            {
                Title = $"{MainWindow.Instance.FindResource("Errore")}",
                Body = $"{MainWindow.Instance.FindResource("OperazioneNonValida")}"
            };

            MainWindow.notification.ShowNotification(not);
            return;
        }
        Image img = this.Find<Image>("carta" + (inizio * 10 + a));
        Image img1 = this.Find<Image>("carta" + (fine * 10 + ++b));
        vettore[fine * 10 + b] = vettore[inizio * 10 + a];
        vettore[inizio * 10 + a] = null;
        img1.Source = img.Source;
        img1.IsVisible = true;
        img.IsVisible = false;
        switch (inizio)
        {
            case 0: i = --a; break;
            case 1: j = --a; break;
            case 2: k = --a; break;
        }
        switch (fine)
        {
            case 0: i = b; break;
            case 1: j = b; break;
            case 2: k = b; break;
        }
        mosse++;
        if (b == 8)
        {
            if (vettore[0] != null) i = 0;
            else if (vettore[10] != null) i = 10;
            else if (vettore[20] != null) i = 20;
            bool continua = true;
            UInt16 x = 0;
            while (continua && x < b - 1)
            {
                if (vettore[i + x].CompareTo(vettore[i + x + 1]) == 1)
                    continua = false;
                x++;
            }
            if (continua)
            {
                Applicazione.IsVisible = false;
                greeting.IsVisible = true;
                msgfine.Content = $"{MainWindow.Instance.FindResource("SolitarioFinito")} {mosse} {MainWindow.Instance.FindResource("VuoiFareNuovaPartita")}";
                condividi.IsEnabled = true;
            }
        }
    }

    private void OnFine_Click(object sender, RoutedEventArgs e)
    {
        MainWindow.Instance.Close();
    }

    private void OnInfo_Click(object sender, RoutedEventArgs e)
    {
        Applicazione.IsVisible = false;
        Info.IsVisible = true;
    }
    private void OnApp_Click(object sender, RoutedEventArgs e)
    {
        Info.IsVisible = false;
        Applicazione.IsVisible = true;
    }
    private void greetingsOk_Click(object sender, RoutedEventArgs e)
    {
        UInt16 a = 0, b = 0;
        ElaboratoreCarteBriscola el = new ElaboratoreCarteBriscola(false, 1,9,9);
        Mazzo m = new Mazzo(el);
        Info.IsVisible = false;
        Applicazione.IsVisible = true;
        Image img;
        for (UInt16 i = 0; i < 29; i++)
        {
            img = this.Find<Image>("carta" + i);
            img.IsVisible = false;
            vettore[i] = null;
        }
        for (UInt16 i = 0; i < 9; i++)
        {
            vettore[a * 10 + b] = Carta.GetCarta(m.GetCarta());
            a++;
            if (a > 2)
            {
                a = 0;
                b++;
            }
        }
        carta0.Source = vettore[0].GetImmagine();
        carta0.IsVisible = true;
        carta1.Source = vettore[1].GetImmagine();
        carta1.IsVisible = true;
        carta2.Source = vettore[2].GetImmagine();
        carta2.IsVisible = true;
        carta10.Source = vettore[10].GetImmagine();
        carta10.IsVisible = true;
        carta11.Source = vettore[11].GetImmagine();
        carta11.IsVisible = true;
        carta12.Source = vettore[12].GetImmagine();
        carta12.IsVisible = true;
        carta20.Source = vettore[20].GetImmagine();
        carta20.IsVisible = true;
        carta21.Source = vettore[21].GetImmagine();
        carta21.IsVisible = true;
        carta22.Source = vettore[22].GetImmagine();
        carta22.IsVisible = true;
        i = j = k = 2;
        greeting.IsVisible = false;
        Applicazione.IsVisible = true;
    }
    private void OnSito_Click(object sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "https://github.com/GiulianoSpaghetti/solitario.avalonia",
            UseShellExecute = true
        };
        Process.Start(psi);
    }
    private void greetingsShare_Click(object sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = $"https://twitter.com/intent/tweet?text=Ho%20completato%20la%20torre%20di%babele%20in%20avalonia%20di%20numerone%20in%20{mosse}%20mosse&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2Fsolitario.Avalonia",
            UseShellExecute = true
        };
        condividi.IsEnabled = false;
        Process.Start(psi);
    }


}
