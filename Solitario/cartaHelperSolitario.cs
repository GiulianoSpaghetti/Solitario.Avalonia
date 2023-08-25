/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 0.1
 *
 *  Created by numerunix on 22/05/22.
 *  Copyright 2022 Some rights reserved.
 *
 */

using Avalonia.Controls;
using System;

namespace Solitario.Avalonia
{
    class cartaHelperSolitario : cartaHelper
    {
        private static cartaHelperSolitario istanza;
        private cartaHelperSolitario() { }
        public static cartaHelperSolitario getIstanza()
        {
            if (istanza == null)
            {
                istanza = new cartaHelperSolitario();
            }
            return istanza;
        }
        public ushort getSeme(ushort carta)
        {
            return (ushort)(carta / 10);
        }
        public ushort getValore(ushort carta)
        {
            return (ushort)(carta % 10);
        }
        public ushort getNumero(ushort seme, ushort valore)
        {
            if (seme > 4 || valore > 9)
                throw new ArgumentException($"Chiamato cartaHelperBriscola::getNumero con seme={seme} e valore={valore}");
            return (ushort)(seme * 10 + valore);
        }

        public int CompareTo(ushort carta, ushort carta1)
        {
                   UInt16 valore = getValore(carta),
                   valore1 = getValore(carta1),
                   semeCarta = getSeme(carta),
                      semeCarta1 = getSeme(carta1);
            if (valore < valore1)
                return 1;
            else if (valore > valore1)
                return -1;
            else
                return 0;
        }
    }
}