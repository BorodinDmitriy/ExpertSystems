using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Integration;

namespace Lab3
{
    public delegate double SubIntegral(double x);
    public static class MathHelper
    {
        public static double Integral(SubIntegral func, double min, double max, int count)
        {
            double res = GaussLegendreRule.Integrate(x => { return func(x); }, min, max, count);
            return res;
        }
    }
}