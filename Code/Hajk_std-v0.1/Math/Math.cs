using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hstd.Math
{
    /// <summary>
    /// Class for provide basic Math functions
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// Compare two of Number, and return bigger.
        /// </summary>
        /// <param name="a">Number A</param>
        /// <param name="b">Number B</param>
        /// <returns></returns> 
        public static T Bigger<T>(T a, T b) where T : IComparable 
        {
            if (a.CompareTo(b) > 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }
}
