using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class SugenoConclusion : Conclusion
    {
        private double Val;

        public SugenoConclusion(string variable, FuzzySet term, double weightAR, double AR, double weightDR, double DR)
        {
            this.Variable = new Variable(variable);
            this.Term = term;
            this.Val = weightAR * AR - weightDR * DR;
        }

        public override double getDamage() {
            return Val;
        }
    }
}
