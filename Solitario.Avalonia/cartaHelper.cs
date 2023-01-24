/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 0.1
 *
 *  Created by numerunix on 22/05/22.
 *  Copyright 2022 Some rights reserved.
 *
 */

using System;
namespace Solitario.Avalonia
{
    interface cartaHelper
    {
        ushort getValore(ushort carta);
        ushort getNumero(ushort seme, ushort valore);
    };
}