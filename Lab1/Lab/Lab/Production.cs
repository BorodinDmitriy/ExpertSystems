using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class Production
    {
        public Production()
        {
            this._Conditions = new List<Condition>();
            this._Facts = new List<Fact>();
        }

        public bool CheckFactInConditions(List<Fact> facts)
        {
            return true;
        }
        

        public void AddFact(Fact newFact)
        {
            this._Facts.Add(newFact);
            return;
        }

        public void AddRangeFacts(List<Fact> newFacts)
        {
            this._Facts.AddRange(newFacts);
            return;
        }

        public void AddCondition(Condition newConditions)
        {
            this._Conditions.Add(newConditions);
            return;
        }

        public void AddRangeConditions(List<Condition> newConditions)
        {
            this._Conditions.AddRange(newConditions);
            return;
        }

        private List<Condition> _Conditions;
        private List<Fact> _Facts;
    }
}
