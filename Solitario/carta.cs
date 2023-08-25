/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 0.1
 *
 *  Created by numerunix on 22/05/22.
 *  Copyright 2022 Some rights reserved.
 *
 */


using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using System.Reflection;

namespace Solitario.Avalonia
{
    class carta
    {
        private ushort seme,
                   valore;
        private cartaHelperSolitario helper;
        private static carta[] carte = new carta[40];
        private Bitmap img;
        private carta(ushort n, cartaHelperSolitario h)
        {
            helper = h;
            seme = helper.getSeme(n);
            valore = helper.getValore(n);
        }
        public static void inizializza(ushort n, cartaHelperSolitario h)
        {
            for (ushort i = 0; i < n; i++)
            {
                carte[i] = new carta(i, h);
            }
            CaricaImmagini(n, h);
        }
        public static carta getCarta(ushort quale) { return carte[quale]; }
        public ushort getSeme() { return seme; }
        public ushort getValore() { return valore; }
        public bool stessoSeme(carta c1) { if (c1 == null) return false; else return seme == c1.getSeme(); }
        public int CompareTo(carta c1)
        {
            if (c1 == null)
                return 1;
            else
                return helper.CompareTo(helper.getNumero(getSeme(), getValore()), helper.getNumero(c1.getSeme(), c1.getValore()));
        }

        public static Bitmap getImmagine(ushort quale)
        {
            return carte[quale].img;
        }

        public Bitmap getImmagine()
        {
            return img;
        }

        public static void CaricaImmagini(ushort n, cartaHelperSolitario helper)
        {
            Stream asset;

            for (ushort i = 0; i < n; i++)
            {
                carte[i].img = new Bitmap(AssetLoader.Open(new Uri($"avares://Solitario/Assets/{i}.png")));
            }
        }
    }
}
