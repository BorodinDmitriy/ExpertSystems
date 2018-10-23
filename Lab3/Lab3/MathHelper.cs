using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public delegate double SubIntegral(double x);
    public static class MathHelper
    {
        public static double Integral(SubIntegral func, double min, double max, int count)
        {
            double h = (max - min) / count;
            double res = 0;

            double x = min + h;

            while (x < max)
            {
                res += 4 * func(x);
                x += h;
                res += 2 * func(x);
                x += h;
            }

            res = (h / 3) * (res + func(min) - func(max));
            return res;
        }
    }
}