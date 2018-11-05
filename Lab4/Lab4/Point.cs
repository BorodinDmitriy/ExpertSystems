using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Point
    {
        public Point(double ar, double dr)
        {
            this._AR = ar;
            this._DR = dr;
            return;
        }
        public double AR
        {
            get { return _AR; }
            set { this._AR = value; }
        }

        public double DR
        {
            get { return _DR; }
            set { this._DR = value; }
        }

        private double _AR;
        private double _DR;
    }
}
