using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Conclusion : Statement
    {
        public Conclusion()
        {
        }
        public Conclusion(string variable, FuzzySet term)
        {
            this.Variable = new Variable(variable);
            this.Term = term;
        }

        public Conclusion(string variable, FuzzySet term, double weight)
        {
            this.Variable = new Variable(variable);
            this.Term = term;
            this._Weight = weight;
        }

        public double Weight
        {
            set { this._Weight = value; }
            get { return this._Weight; }
        }

        public virtual double getDamage()
        {
            return 0;
        }

        private double _Weight = 1;
    }
}
