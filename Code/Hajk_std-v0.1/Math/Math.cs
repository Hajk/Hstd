using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hajk_std.Math
{
    public static class Math
    {
        /// <summary>
        /// Porovná a vrátí větší ze dvou čísel
        /// </summary>
        /// <param name="cislo1">číslo 1</param>
        /// <param name="cislo2">číslo 2</param>
        /// <returns></returns>
        public static int Vetsi(int cislo1, int cislo2)
        {
            if (cislo1 < cislo2)
            {
                return cislo2;
            }
            else
            {
                return cislo1;
            }
        }

        /// <summary>
        /// Porovná a vrátí větší ze dvou čísel
        /// </summary>
        /// <param name="cislo1">číslo 1</param>
        /// <param name="cislo2">číslo 2</param>
        /// <returns></returns>
        public static double Vetsi(double cislo1, double cislo2)
        {
            if (cislo1 < cislo2)
            {
                return cislo2;
            }
            else
            {
                return cislo1;
            }
        }
    }
}
