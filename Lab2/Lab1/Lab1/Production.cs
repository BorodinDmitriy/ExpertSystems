using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Production
    {
        public Production()
        {
            this._Conditions = new List<Fact>();
            this._Facts = new List<Fact>();
        }

        public bool CheckFactInConditions(List<Fact> facts)
        {
            for (int I = 0; I < this._Conditions.Count; I++)
            {
                int index = facts.FindIndex((item) =>
                {
                    return this._Conditions[I].Name == item.Name;
                });

                if (index < 0)
                    return false;
            }
            return true;
        }

        public bool CheckConditions(List<Fact> facts)
        {
            for (int I = 0; I < this._Conditions.Count; I++)
            {
                Fact curFact = facts.Find((item) => { return item.Name == this._Conditions[I].Name; });
                if (curFact == null)
                    return false;
                bool state = this._Conditions[I].CheckCondition(curFact);
                if (!state)
                {
                    return false;
                }
            }
            return true;
        }
        
        public List<Fact> Facts
        {
            get
            {
                return this._Facts;
            }
        }

        public List<Fact> Conditions
        {
            get
            {
                return this._Conditions;
            }
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

        public void AddCondition(Fact newConditions)
        {
            this._Conditions.Add(newConditions);
            return;
        }

        public void AddRangeConditions(List<Fact> newConditions)
        {
            this._Conditions.AddRange(newConditions);
            return;
        }

        private List<Fact> _Conditions;
        private List<Fact> _Facts;
    }
}
