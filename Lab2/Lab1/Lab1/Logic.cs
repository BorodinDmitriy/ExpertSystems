using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Logic
    {
        public Logic()
        {
            this._Productions = new List<Production>();
            this._MaxDeep = 8;
            this._CurrentDeep = 0;
            InitializeProduction();

        }

        public void AddProduction(Production newItem)
        {
            this._Productions.Add(newItem);
            return;
        }

        public bool DirectOutput(List<Fact> goals, List<Fact> facts)
        {
            bool result = false;

            this._CurrentDeep = 0;

            //  П.1 Проверка цели и фактов
            result = CheckGoals(goals, facts);
            if (result)
                return result;

            result = DirectLoop(goals, facts);
            return result;
        }

        public bool ReverseOutput(List<Fact> goals, List<Fact> facts)
        {
            bool result = false;

            this._CurrentDeep = 0;

            //  П.1 Проверка цели и фактов
            result = CheckGoals(goals, facts);
            if (result)
                return result;

            result = ReverseLoop(goals, facts);
            return result;
        }
 
        private List<Production> _Productions;
        private int _MaxDeep;
        private int _CurrentDeep;


        private bool DirectLoop(List<Fact> goals, List<Fact> facts)
        {
            bool state = false;
            int start = 0;
            while(!state)
            {
                int point = FindProductions(facts, start);
                if (point == -1)
                {
                    break;
                }
                start = point + 1;
                bool status = this._Productions[point].CheckConditions(facts);
                if (!status)
                {
                    continue;
                }
                List<Fact> localFacts = new List<Fact>();
                localFacts.AddRange(facts);
                AddUniqFact(ref localFacts, this._Productions[point].Facts);


                state = CheckGoals(goals, localFacts);
                if (state)
                {
                    return state;
                }

                if (this._CurrentDeep + 1 > this._MaxDeep)
                {
                    break;
                }

                this._CurrentDeep++;
                state = DirectLoop(goals, localFacts);
                if (state)
                {
                    this._CurrentDeep--;
                    return true;
                }
            }
            this._CurrentDeep--;
            return false;
        }

        private bool ReverseLoop(List<Fact> goals, List<Fact> facts)
        {
            bool state = false;
            int start = 0;
            while (!state)
            {
                int point = FindProductionsByGoal(goals, start);
                if (point == -1)
                {
                    break;
                }
                start = point + 1;
                List<Condition> conditions = this._Productions[point].Conditions;

                List<Condition> localGoals = resolveConditions(conditions, facts);
                if (localGoals.Count == 0)
                {
                    this._CurrentDeep--;
                    return true;
                }

                state = ReverseLoop(localGoals, facts);
                if (state)
                {
                    this._CurrentDeep--;
                    return state;
                }
            }
            this._CurrentDeep--;
            return false;
        }

        private bool ReverseLoop(List<Condition> goals, List<Fact> facts)
        {
            bool state = false;
            int start = 0;
            while (!state)
            {
                int point = FindProductionsByGoal(goals, start);
                if (point == -1)
                {
                    break;
                }
                start = point + 1;
                List<Condition> conditions = this._Productions[point].Conditions;
                conditions.AddRange(goals);

                List<Condition> localGoals = resolveConditions(conditions, facts);
                if (localGoals.Count == 0)
                {
                    this._CurrentDeep--;
                    return true;
                }

                state = ReverseLoop(localGoals, facts);
                if (state)
                {
                    this._CurrentDeep--;
                    return state;
                }
            }
            this._CurrentDeep--;
            return false;
        }



        /*private List<Condition> resolveConditions(List<Condition> conditions, List<Fact> facts)
        {
            
        }*/

        private int FindProductionsByGoal(List<Condition> goals, int start)
        {
            for (int I = 0; I < this._Productions.Count; I++)
            {
                List<Fact> facts = this._Productions[I].Facts;
                for (int J = 0; J < facts.Count; J++)
                {
                    int index = goals.FindIndex((item) => { return ((item.Name == facts[J].Name) && (item.Min >= facts[J].Value) && (item.Max <= facts[J].Value)); });
                    if (index >= 0)
                    {
                        return index;
                    }
                }
            }
            return -1;
        }

        private List<Condition> resolveConditions(List<Condition> conditions, List<Fact> facts)
        {
            List<Condition> remainingGoals = new List<Condition>();
            for (int I = 0; I < conditions.Count; I++)
            {
                int index = facts.FindIndex((item) => { return item.Name == conditions[I].Name; });
                bool state = conditions[I].CheckCondition(facts[index]);
                if (!state)
                {
                    remainingGoals.Add(conditions[I]);
                }
            }
            return remainingGoals;
        }

        private int FindProductionsByGoal(List<Fact> goals, int start)
        {
            for (int I = start; I < this._Productions.Count; I++)
            {
                for (int J = 0; J < goals.Count; J++)
                {
                    int index = this._Productions[I].Facts.FindIndex((item) => { return ((item.Name == goals[J].Name) && (item.Value == goals[J].Value)); });
                    if (index >= 0)
                    {
                        return I;
                    }
                }
            }
            return -1;
        }

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

        private void AddUniqFact(ref List<Fact> dest, List<Fact> src)
        {
            for (int I = 0; I < src.Count; I++)
            {
                int index = dest.FindIndex((item) => { return item.Name == src[I].Name; });
                if (index < 0)
                {
                    dest.Add(src[I]);
                }
            }
            return;
        }

        private void InitializeProduction()
        {
            // One
            Production item = new Production();
            item.AddCondition(new Condition("A1", 0, 1));
            item.AddCondition(new Condition("B1", 2, 3));
            item.AddCondition(new Condition("C1", 0, 0));
            item.AddCondition(new Condition("A2", 1, 1));
            item.AddCondition(new Condition("B2", 2, 2));
            item.AddCondition(new Condition("C2", 0, 0));
            item.AddCondition(new Condition("A3", 1, 2));
            item.AddCondition(new Condition("B3", 3, 3));
            item.AddCondition(new Condition("C3", 1, 2));

            item.AddFact(new Fact("Number", 1));

            this._Productions.Add(item);

            //  Two
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 2, 2));
            item.AddCondition(new Condition("C1", 2, 2));
            item.AddCondition(new Condition("A2", 0, 1));
            item.AddCondition(new Condition("B2", 1, 2));
            item.AddCondition(new Condition("C2", 0, 1));
            item.AddCondition(new Condition("A3", 0, 3));
            item.AddCondition(new Condition("B3", 2, 3));
            item.AddCondition(new Condition("C3", 0, 2));

            item.AddFact(new Fact("Number", 2));

            this._Productions.Add(item);

            //  Three
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 1, 3));
            item.AddCondition(new Condition("C1", 0, 3));
            item.AddCondition(new Condition("A2", 0, 2));
            item.AddCondition(new Condition("B2", 1, 3));
            item.AddCondition(new Condition("C2", 0, 3));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 2, 3));
            item.AddCondition(new Condition("C3", 0, 3));

            item.AddFact(new Fact("Number", 3));

            this._Productions.Add(item);
            //  Four
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 1));
            item.AddCondition(new Condition("B1", 1, 2));
            item.AddCondition(new Condition("C1", 2, 2));
            item.AddCondition(new Condition("A2", 1, 3));
            item.AddCondition(new Condition("B2", 2, 2));
            item.AddCondition(new Condition("C2", 2, 3));
            item.AddCondition(new Condition("A3", 0, 0));
            item.AddCondition(new Condition("B3", 0, 0));
            item.AddCondition(new Condition("C3", 2, 2));

            item.AddFact(new Fact("Number", 4));

            this._Productions.Add(item);

            //  Five
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 3));
            item.AddCondition(new Condition("B1", 2, 3));
            item.AddCondition(new Condition("C1", 1, 2));
            item.AddCondition(new Condition("A2", 0, 2));
            item.AddCondition(new Condition("B2", 2, 2));
            item.AddCondition(new Condition("C2", 0, 2));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 2, 2));
            item.AddCondition(new Condition("C3", 0, 2));

            item.AddFact(new Fact("Number", 5));

            this._Productions.Add(item);

            //  Six
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 2, 2));
            item.AddCondition(new Condition("C1", 0, 2));
            item.AddCondition(new Condition("A2", 0, 3));
            item.AddCondition(new Condition("B2", 2, 3));
            item.AddCondition(new Condition("C2", 0, 2));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 2, 2));
            item.AddCondition(new Condition("C3", 0, 2));

            item.AddFact(new Fact("Number", 6));

            this._Productions.Add(item);

            //  Seven
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 2, 3));
            item.AddCondition(new Condition("C1", 0, 3));
            item.AddCondition(new Condition("A2", 0, 0));
            item.AddCondition(new Condition("B2", 0, 2));
            item.AddCondition(new Condition("C2", 0, 2));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 2, 2));
            item.AddCondition(new Condition("C3", 0, 0));

            item.AddFact(new Fact("Number", 7));

            this._Productions.Add(item);

            //  Eight
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 2, 2));
            item.AddCondition(new Condition("C1", 0, 2));
            item.AddCondition(new Condition("A2", 0, 2));
            item.AddCondition(new Condition("B2", 2, 2));
            item.AddCondition(new Condition("C2", 0, 2));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 2, 2));
            item.AddCondition(new Condition("C3", 0, 2));

            item.AddFact(new Fact("Number", 8));

            this._Productions.Add(item);

            //  Nine
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 2));
            item.AddCondition(new Condition("B1", 2, 2));
            item.AddCondition(new Condition("C1", 0, 2));
            item.AddCondition(new Condition("A2", 0, 1));
            item.AddCondition(new Condition("B2", 1, 3));
            item.AddCondition(new Condition("C2", 0, 3));
            item.AddCondition(new Condition("A3", 0, 2));
            item.AddCondition(new Condition("B3", 0, 2));
            item.AddCondition(new Condition("C3", 0, 0));

            item.AddFact(new Fact("Number", 9));

            this._Productions.Add(item);

            //  Zero
            item = new Production();
            item.AddCondition(new Condition("A1", 0, 3));
            item.AddCondition(new Condition("B1", 2, 3));
            item.AddCondition(new Condition("C1", 0, 3));
            item.AddCondition(new Condition("A2", 0, 2));
            item.AddCondition(new Condition("B2", 0, 2));
            item.AddCondition(new Condition("C2", 0, 2));
            item.AddCondition(new Condition("A3", 0, 3));
            item.AddCondition(new Condition("B3", 2, 3));
            item.AddCondition(new Condition("C3", 0, 2));

            item.AddFact(new Fact("Number", 0));

            this._Productions.Add(item);
        }
    }
}
