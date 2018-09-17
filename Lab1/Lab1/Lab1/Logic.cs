using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class Logic
    {
        public Logic()
        {

        }

        public bool DirectOutput(List<Fact> goals, List<Fact> facts)
        {
            bool result = false;

            List<Fact> localFacts = new List<Fact>();
            localFacts.AddRange(facts);

            //  П.1 Проверка цели и фактов
            result = CheckGoals(goals, facts);
            if (result)
                return result;

            List<int> pointer = new List<int>();

            bool state = false;
            int start = 0;

            while (!state)
            {
                
                int point = FindProductions(localFacts, start);
                if (point == -1)
                {
                    if (pointer.Count == 0)
                    {
                        state = true;
                    }
                    else
                    {
                        start = pointer.Last();
                        pointer.RemoveAt(pointer.Count);
                        continue;
                    }
                }

                pointer.Add(point + 1);
                start = point + 1;

                bool status = _Productions[point].CheckConditions(localFacts);
                if (!status)
                {
                    continue;
                }
                localFacts.AddRange(this._Productions[point].Facts);
            }

            return result;
        }


 
        private List<Production> _Productions = new List<Production>();

        private bool CheckGoals(List<Fact> goals, List<Fact> facts)
        {
            bool result = true;

            for (int I = 0; I < goals.Count; I++)
            {
                Fact currectGoal = goals[I];
                Fact fact = facts.Find((item) =>
                {
                    return ((item.Name == currectGoal.Name) && (item.Value == currectGoal.Value));
                });

                if (fact == null)
                {
                    return false;
                }
            }

            return result;
        }

        private int FindProductions(List<Fact> facts, int start)
        {
            bool state = false;
            for (int I = start; I < _Productions.Count; I++)
            {
                state = _Productions[I].CheckFactInConditions(facts);
                if (state)
                {
                    return I;
                }
            }

            return -1;
        }
    }
}
