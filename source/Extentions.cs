using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineParameters
{
    public static class Extentions
    {
        public static double ToDouble (this string str)
        {
            double result;
            double.TryParse(str, out result);
            return result;
        }
    }
}
