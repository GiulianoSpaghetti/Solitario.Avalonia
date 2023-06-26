using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;

namespace Solitario.Avalonia
{
    public partial class MainWindow : Window
    {
        UInt16 i, j, k;
        private carta[] vettore;
        private ulong mosse = 0;
        public MainWindow()
        {
            int a = 0, b = 0;
            InitializeComponent();
            vettore = new carta[30];
            elaboratoreCarteSolitario e = new elaboratoreCarteSolitario();
            mazzo m = new mazzo(e);
            IAssetLoader assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            m.setNome("Napoletano");
            carta.inizializza(10, cartaHelperSolitario.getIstanza(), assets);

            for (UInt16 i = 0; i < 9; i++)
            {
                vettore[a * 10 + b] = carta.getCarta(m.getCarta());
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            carta0.Source = vettore[0].getImmagine();
            carta0.IsVisible = true;
            carta1.Source = vettore[1].getImmagine();
            carta1.IsVisible = true;
            carta2.Source = vettore[2].getImmagine();
            carta2.IsVisible = true;
            carta10.Source = vettore[10].getImmagine();
            carta10.IsVisible = true;
            carta11.Source = vettore[11].getImmagine();
            carta11.IsVisible = true;
            carta12.Source = vettore[12].getImmagine();
            carta12.IsVisible = true;
            carta20.Source = vettore[20].getImmagine();
            carta20.IsVisible = true;
            carta21.Source = vettore[21].getImmagine();
            carta21.IsVisible = true;
            carta22.Source = vettore[22].getImmagine();
            carta22.IsVisible = true;
            i = j = k = 2;
            ok.Content = this.FindResource("Ok");
            Prompt.Content = this.FindResource("SpostaCarta");
            Prompt1.Content = this.FindResource("SuRiga");
            info.Content = this.FindResource("Informazioni");
            greetingsClose.Content = this.FindResource("Annulla");
            greetingsOk.Content = this.FindResource("Ok");
            condividi.Content = this.FindResource("Condividi");
            InfoApp.Content = this.FindResource("Applicazione");
        }

        private void OnOk_Click(object sender, RoutedEventArgs e)
        {
            UInt16 inizio, fine, a = 0, b = 0;
            Risultato.Content = "";
            Risultato.Foreground = Brushes.Black;
            if (Inizio.Text == null || Inizio.Text == "") { Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiInizio")} {this.FindResource("EVuota")}"; Risultato.Foreground = Brushes.Red; return; }

            if (Fine.Text == null || Fine.Text == "") { Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiFine")} {this.FindResource("EVuota")}"; Risultato.Foreground = Brushes.Red; return; }

            try
            {
                inizio = UInt16.Parse(Inizio.Text);
            }
            catch (FormatException ex)
            {
                Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiInizio")} {this.FindResource("NonIntera")}"; Risultato.Foreground = Brushes.Red; return;
            }
            inizio--;
            try
            {
                fine = UInt16.Parse(Fine.Text);
            }
            catch (FormatException ex)
            {
                Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiFine")} {this.FindResource("NonIntera")}"; Risultato.Foreground = Brushes.Red; return;
            }
            fine--;
            if (inizio > 2) { Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiInizio")} {this.FindResource("NonNelRange")}"; Risultato.Foreground = Brushes.Red; return; }
            if (fine > 2) { Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiInizio")} {this.FindResource("NonNelRange")}"; Risultato.Foreground = Brushes.Red; return; }
            if (inizio == fine) { Risultato.Content = $"{this.FindResource("LeRigheCoincidono")}"; Risultato.Foreground = Brushes.Red; return; }
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
            carta c;
            try
            {
                c = vettore[inizio * 10 + a];
            } catch (System.IndexOutOfRangeException ex)
            {
                Risultato.Content = $"{this.FindResource("LaRiga")} {this.FindResource("DiInizio")} {this.FindResource("EVuota")}"; Risultato.Foreground = Brushes.Red; return;
            }
            carta c1 = vettore[fine * 10 + ++b];
            if (c.CompareTo(c1) == -1) { Risultato.Content = $"{this.FindResource("OperazioneNonValida")}"; Risultato.Foreground = Brushes.Red; return; }
            Image img = this.Find<Image>("carta" + (inizio * 10 + a));
                Image img1 = this.Find<Image>("carta" + (fine * 10 + b));
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
                if (b == 8) {
                    if (vettore[0] != null) i = 0;
                    else if (vettore[10] != null) i = 10;
                    else if (vettore[20] != null) i = 20;
                    bool continua = true;
                    UInt16 x = 0;
                    while (continua && x < b - 1) {
                        if (vettore[i + x].CompareTo(vettore[i + x + 1]) == 1)
                            continua = false;
                        x++;
                    }
                    if (continua)
                    {
                        Applicazione.IsVisible = false;
                        greeting.IsVisible = true;
                        msgfine.Content = $"{this.FindResource("SolitarioFinito")} {mosse} {this.FindResource("VuoiFareNuovaPartita")}";
                        condividi.IsEnabled = true;
                    }
                }
            }

        private void OnFine_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
            UInt16 a=0, b=0;
            elaboratoreCarteSolitario el = new elaboratoreCarteSolitario();
            mazzo m = new mazzo(el);
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
                vettore[a * 10 + b] = carta.getCarta(m.getCarta());
                a++;
                if (a > 2)
                {
                    a = 0;
                    b++;
                }
            }
            carta0.Source = vettore[0].getImmagine();
            carta0.IsVisible = true;
            carta1.Source = vettore[1].getImmagine();
            carta1.IsVisible = true;
            carta2.Source = vettore[2].getImmagine();
            carta2.IsVisible = true;
            carta10.Source = vettore[10].getImmagine();
            carta10.IsVisible = true;
            carta11.Source = vettore[11].getImmagine();
            carta11.IsVisible = true;
            carta12.Source = vettore[12].getImmagine();
            carta12.IsVisible = true;
            carta20.Source = vettore[20].getImmagine();
            carta20.IsVisible = true;
            carta21.Source = vettore[21].getImmagine();
            carta21.IsVisible = true;
            carta22.Source = vettore[22].getImmagine();
            carta22.IsVisible = true;
            i = j = k = 2;
            greeting.IsVisible = false;
            Applicazione.IsVisible = true;
        }

        private void greetingsShare_Click(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = $"https://twitter.com/intent/tweet?text=Ho%20completato%20il%20solitario%20di%20numerone%20in%20{mosse}%20mosse&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2Fsolitario.Avalonia",
                UseShellExecute = true
            };
            condividi.IsEnabled = false;
            Process.Start(psi);
        }


    }
}
