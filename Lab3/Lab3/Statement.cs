using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Statement
    {
        public Variable Variable
        {
            set { this.variable = value; }
            get { return this.variable; }
        }

        public FuzzySet Term
        {
            set { this.term = value; }
            get { return this.term; }
        }

        private Variable variable;
        private FuzzySet term;        
    }
}
