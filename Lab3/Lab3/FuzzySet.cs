using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X;
        public double Y;
    }
    public class FuzzySet
    {

        public virtual double GetValue(double x)
        {
            return 0;
        }
        protected double PointProjectionOnLine(Point A, Point B, double M)
        {
            double res = ((M - A.X) * (B.Y - A.Y) / (B.X - A.X)) + A.Y;
            return res;
        }
    }

    public class TriangleSet: FuzzySet
    {
        public TriangleSet(Point a, Point b, Point c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        private Point A;
        private Point B;
        private Point C;
        public override double GetValue(double x)
        {
            double res = 0;
            if (x < A.X || x > C.X)
            {
                return res;
            }

            if (x == A.X && x != B.X)
            {
                return A.Y;
            }

            if (x == A.X && x == B.X)
            {
                return Math.Max(A.Y, B.Y);
            }

            if (x == B.X && x != C.X)
            {
                return B.Y;
            }

            if (x == B.X && x == C.X)
            {
                return Math.Max(B.Y, C.Y);
            }

            if (x == C.X)
            {
                return C.Y;
            }

            if (x > A.X && x < B.X)
            {
                res = PointProjectionOnLine(A, B, x);
                return res;
            }

            if (x > B.X && x < C.X)
            {
                res = PointProjectionOnLine(B, C, x);
                return res;
            }

            return res;
        }

        
    }

    public class TrapezeSet: FuzzySet
    {
        public TrapezeSet(Point a, Point b, Point c, Point d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        public override double GetValue(double x)
        {
            double res = 0;

            if (x < A.X || x > D.X)
            {
                return res;
            }

            if (x == A.X && x != B.X)
            {
                return A.Y;
            }

            if (x == A.X && x == B.X)
            {
                return Math.Max(A.Y, B.Y);
            }

            if (x == B.X && x != C.X)
            {
                return B.Y;
            }

            if (x== B.X && x == C.X)
            {
                return Math.Max(B.Y, C.Y);
            }

            if (x == C.X && x != D.X)
            {
                return C.Y;
            }

            if (x == C.X && x == D.X)
            {
                return Math.Max(C.Y, D.Y);
            }

            if (x == D.X)
            {
                return D.Y;
            }

            if (x > A.X && x < B.X)
            {
                res = PointProjectionOnLine(A, B, x);
                return res;
            }

            if (x > B.X && x < C.X) 
            {
                return B.Y;
            }

            if (x > C.X && x < D.X)
            {
                res = PointProjectionOnLine(C, D, x);
                return res;
            }

            return res;
        }

        private Point A;
        private Point B;
        private Point C;
        private Point D;
    }

    public class ActiveFuzzySet: FuzzySet
    {
        public ActiveFuzzySet(double truthDegree, FuzzySet parent)
        {
            this._TruthDegree = truthDegree;
            this._linkOnTerm = parent;
        }

        public double TruthDegree
        {
            set { this._TruthDegree = value; }
            get { return this._TruthDegree; }
        }

        public FuzzySet Term
        {
            set { this._linkOnTerm = value; }
            get { return this._linkOnTerm; }
        }

        public override double GetValue(double x)
        {
            return Math.Min(this._TruthDegree, this._linkOnTerm.GetValue(x));
        }

        private double _TruthDegree;
        private FuzzySet _linkOnTerm;

    }

    public class UnionFuzzySet: FuzzySet
    {
        private List<FuzzySet> union = new List<FuzzySet>();

        public List<FuzzySet> Union
        {
            set { this.union = value; }
            get { return this.union; }
        }

        public override double GetValue(double x)
        {
            double res = 0;
            for (int I = 0; I < this.union.Count; I++)
            {
                res = Math.Max(res, union[I].GetValue(x));
            }
            return res;
        }
    }
}
