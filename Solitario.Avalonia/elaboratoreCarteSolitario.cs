/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 0.1
 *
 *  Created by numerunix on 22/05/22.
 *  Copyright 2022 Some rights reserved.
 *
 */

using System;
using Avalonia.Interactivity;
namespace Solitario.Avalonia
{
    class elaboratoreCarteSolitario : elaboratoreCarte
    {
        private const ushort numeroCarte = 10;
        private bool[] doppione;
        public static Random r = new Random();
        public elaboratoreCarteSolitario()
        {
            doppione = new bool[40];
            for (int i = 0; i < 40; i++)
                doppione[i] = false;
        }
        public ushort getCarta()
        {
            ushort fine = (ushort)(r.Next(0, 9) % numeroCarte),
            carta = (ushort)((fine + 1) % numeroCarte);
            while (doppione[carta] && carta != fine)
                carta = (ushort)((carta + 1) % numeroCarte);
            if (doppione[carta])
                throw new ArgumentException("Chiamato elaboratoreCarteItaliane::getCarta() quando non ci sono più carte da elaborare");
            else
            {
                doppione[carta] = true;
                return carta;
            }
        }

    }
}