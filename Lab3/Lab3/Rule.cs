using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Rule
    {
        public List<Condition> Conditions
        {
            set { this._Conditions = value; }
            get { return this._Conditions; }
        }

        public List<Conclusion> Conclusions
        {
            set { this._Conclusions = value; }
            get { return this._Conclusions; }
        }

        private List<Conclusion> _Conclusions;
        private List<Condition> _Conditions;
    }
}
