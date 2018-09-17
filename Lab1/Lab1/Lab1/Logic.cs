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

            List<int> pointer = new List<int>();

            //  П.1 Проверка цели и фактов
            result = CheckGoals(goals, facts);
            if (result)
                return result;



            return result;
        }


 
        private List<Production> _Productions;

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
    }
}
