using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    public class Logic
    {
        public Logic()
        {
            this._Productions = new List<Production>();
            this._MaxDeep = 200;
            this._CurrentDeep = 0;
            this._Trace = new List<string>();
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
            File.Delete("output.txt");

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
            File.Delete("output.txt");

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
        private List<string> _Trace;


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

                status = this.hasGoal(facts, this._Productions[point].Facts);
                if (status)
                {
                    continue;
                }

                File.AppendAllText("output.txt","Добавлены факты из правила №" + point + " , текущая глубина: " + this._CurrentDeep + "\r\n");
                printFactList(facts);
                File.AppendAllText("output.txt", "\r\n");
                printGoalList(goals);
                File.AppendAllText("output.txt", "\r\n\r\n");
                List<Fact> localFacts = new List<Fact>();
                localFacts.AddRange(facts);
                AddUniqFact(ref localFacts, this._Productions[point].Facts);


                state = CheckGoals(goals, localFacts);
                if (state)
                {
                    File.AppendAllText("output.txt","Цель достигнута\r\n");
                    return state;
                }

                if (this._CurrentDeep + 1 > this._MaxDeep)
                {
                    File.AppendAllText("output.txt","Достигнут лимит количества итераций\r\n");
                    break;
                }

                this._CurrentDeep++;
                state = DirectLoop(goals, localFacts);
                if (state)
                {
                    this._CurrentDeep--;
                    return true;
                }
                File.AppendAllText("output.txt","Из списка фактов убраны добавленные факты, которые содержатся в правиле №" + point + "\r\n");
            }
            File.AppendAllText("output.txt","Нет доступных правил\r\n");
            this._CurrentDeep--;
            return false;
        }

        private bool ReverseLoop(List<Fact> goals, List<Fact> facts)
        {
            bool state = false;
            int start = 0;
            while (!state)
            {
                int[] solve = FindProductionsByGoal(goals, start);
                int point = solve[0];
                int goalIndex = solve[1];

                if (point == -1)
                {
                    break;
                }

                start = point + 1;
                List<Fact> localConditions = this._Productions[point].Conditions;
                for (int I = 0; I < goals.Count; I++)
                    if (I != goalIndex)
                        localConditions.Add(goals[I]);

                File.AppendAllText("output.txt","Найдено правило №" + point + " в котором есть одна из целей\r\n");
                printFactList(facts);
                File.AppendAllText("output.txt", "\r\n");
                printGoalList(localConditions);
                File.AppendAllText("output.txt", "\r\n\r\n");
                List<Fact> localGoals = resolveConditions(localConditions, facts);
                if (localGoals.Count == 0)
                {
                    File.AppendAllText("output.txt","В списке целей, не осталось целей\r\n");
                    this._CurrentDeep--;
                    return true;
                }

                state = this.ReverseLoop(localGoals, facts);
                if (state)
                {
                    this._CurrentDeep--;
                    return state;
                }
            }
            File.AppendAllText("output.txt","Не найдено подходящих правил\r\n");
            this._CurrentDeep--;
            return false;
        }       

        private List<Fact> resolveConditions(List<Fact> conditions, List<Fact> facts)
        {
            List<Fact> remainingGoals = new List<Fact>();
            for (int I = 0; I < conditions.Count; I++)
            {
                int index = facts.FindIndex((item) => { return item.Name == conditions[I].Name; });
                if (index < 0)
                {
                    remainingGoals.Add(conditions[I]);
                    continue;
                }
                bool state = conditions[I].CheckCondition(facts[index]);
                if (!state)
                {
                    remainingGoals.Add(conditions[I]);
                }
            }
            return remainingGoals;
        }
        private void printFactList(List<Fact> facts)
        {
            File.AppendAllText("output.txt", "Список фактов: \r\n");
            for (int I = 0; I < facts.Count; I++)
                File.AppendAllText("output.txt", "" + facts[I].Name + " = " + facts[I].Value + "\t");
        }

        private void printGoalList(List<Fact> goals)
        {
            File.AppendAllText("output.txt", "Список целей: \r\n");
            for (int I = 0; I < goals.Count; I++)
                File.AppendAllText("output.txt", "" + goals[I].Name + " " + goals[I].Predicat + " " + goals[I].Value + "\t");
        }

        private int[] FindProductionsByGoal(List<Fact> goals, int start)
        {
            for (int I = start; I < this._Productions.Count; I++)
            {
                for (int J = 0; J < goals.Count; J++)
                {
                    int index = this._Productions[I].Facts.FindIndex((item) => { return ((item.Name == goals[J].Name) && (item.Value == goals[J].Value)); });
                    if (index >= 0)
                    {
                        return new int[] {I, J};
                    }
                }
            }
            return new int[] { -1, -1};
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

        private bool hasGoal(List<Fact> facts, List<Fact> newFacts)
        {
            for (int I = 0; I < newFacts.Count; I++)
            {
                int index = facts.FindIndex((item) => { return ((item.Name == newFacts[I].Name) && (item.Value == newFacts[I].Value)); });
                if (index < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void InitializeProduction()
        {
            //  A1 

            // 1,4
            Production item = new Production();
            item.AddCondition(new Fact("A1", ">=", 0));
            item.AddCondition(new Fact("A1", "<=", 1));
            item.AddFact(new Fact("A1:1,4", "==", 1));

            this._Productions.Add(item);

            // 2,3,6,7,8,9
            item = new Production();
            item.AddCondition(new Fact("A1", ">=", 0));
            item.AddCondition(new Fact("A1", "<=", 2));
            item.AddFact(new Fact("A1:2,3,6,7,8,9", "==" ,1));

            this._Productions.Add(item);

            // 5, 0
            item = new Production();
            item.AddCondition(new Fact("A1", ">=", 0));
            item.AddCondition(new Fact("A1", "<=", 3));
            item.AddFact(new Fact("A1:5,0", "==", 1));

            this._Productions.Add(item);

            //  B1

            //  1, 5, 7, 0
            item = new Production();
            item.AddCondition(new Fact("B1", ">=", 2));
            item.AddCondition(new Fact("B1", "<=", 3));
            item.AddFact(new Fact("B1:1,5,7,0", "==", 1));

            this._Productions.Add(item);

            // 2, 6, 8, 9

            item = new Production();
            item.AddCondition(new Fact("B1", "==", 2));
            item.AddFact(new Fact("B1:2,6,8,9", "==", 1));

            this._Productions.Add(item);

            //  3

            item = new Production();
            item.AddCondition(new Fact("B1", ">=", 1));
            item.AddCondition(new Fact("B1", "<=", 3));
            item.AddFact(new Fact("B1:3", "==", 1));

            this._Productions.Add(item);

            //  4

            item = new Production();
            item.AddCondition(new Fact("B1", ">=", 1));
            item.AddCondition(new Fact("B1", "<=", 2));
            item.AddFact(new Fact("B1:4", "==", 1));

            this._Productions.Add(item);

            //  C1

            //  1
            item = new Production();
            item.AddCondition(new Fact("C1", "==", 0));
            item.AddFact(new Fact("C1:1", "==", 1));

            this._Productions.Add(item);

            //  2, 4
            item = new Production();
            item.AddCondition(new Fact("C1", "==", 2));
            item.AddFact(new Fact("C1:2,4", "==", 1));

            this._Productions.Add(item);

            //  3, 7, 0
            item = new Production();
            item.AddCondition(new Fact("C1", ">=", 0));
            item.AddCondition(new Fact("C1", "=<", 3));
            item.AddFact(new Fact("C1:3,7,0", "==", 1));

            this._Productions.Add(item);

            //  5
            item = new Production();
            item.AddCondition(new Fact("C1", ">=", 1));
            item.AddCondition(new Fact("C1", "=<", 2));
            item.AddFact(new Fact("C1:5", "==", 1));

            this._Productions.Add(item);

            //  6, 8, 9
            item = new Production();
            item.AddCondition(new Fact("C1", ">=", 0));
            item.AddCondition(new Fact("C1", "=<", 2));
            item.AddFact(new Fact("C1:6,8,9", "==", 1));

            this._Productions.Add(item);

            // A2

            //  1
            item = new Production();
            item.AddCondition(new Fact("A2", "==", 1));
            item.AddFact(new Fact("A2:1", "==", 1));

            this._Productions.Add(item);

            //  2, 9
            item = new Production();
            item.AddCondition(new Fact("A2", ">=", 0));
            item.AddCondition(new Fact("A2", "=<", 1));
            item.AddFact(new Fact("A2:2,9", "==", 1));

            this._Productions.Add(item);

            //  3, 5, 8, 0
            item = new Production();
            item.AddCondition(new Fact("A2", ">=", 0));
            item.AddCondition(new Fact("A2", "=<", 2));
            item.AddFact(new Fact("A2:3,5,8,0", "==", 1));

            this._Productions.Add(item);

            //  4
            item = new Production();
            item.AddCondition(new Fact("A2", ">=", 1));
            item.AddCondition(new Fact("A2", "=<", 3));
            item.AddFact(new Fact("A2:4", "==", 1));

            this._Productions.Add(item);

            //  6
            item = new Production();
            item.AddCondition(new Fact("A2", ">=", 0));
            item.AddCondition(new Fact("A2", "=<", 3));
            item.AddFact(new Fact("A2:6", "==", 1));

            this._Productions.Add(item);

            //  7
            item = new Production();
            item.AddCondition(new Fact("A2", ">=", 1));
            item.AddCondition(new Fact("A2", "=<", 2));
            item.AddFact(new Fact("A2:7", "==", 1));

            this._Productions.Add(item);

            //  B2

            //  1, 4, 5, 8
            item = new Production();
            item.AddCondition(new Fact("B2", "==", 2));
            item.AddFact(new Fact("B2:1,4,5,8", "==", 1));

            this._Productions.Add(item);

            //  2
            item = new Production();
            item.AddCondition(new Fact("B2", ">=", 1));
            item.AddCondition(new Fact("B2", "<=", 2));
            item.AddFact(new Fact("B2:2", "==", 1));

            this._Productions.Add(item);

            //  3, 9
            item = new Production();
            item.AddCondition(new Fact("B2", ">=", 1));
            item.AddCondition(new Fact("B2", "<=", 3));
            item.AddFact(new Fact("B2:3,9", "==", 1));

            this._Productions.Add(item);

            //  6
            item = new Production();
            item.AddCondition(new Fact("B2", ">=", 2));
            item.AddCondition(new Fact("B2", "<=", 3));
            item.AddFact(new Fact("B2:6", "==", 1));

            this._Productions.Add(item);

            //  7, 0
            item = new Production();
            item.AddCondition(new Fact("B2", ">=", 0));
            item.AddCondition(new Fact("B2", "<=", 2));
            item.AddFact(new Fact("B2:7,0", "==", 1));

            this._Productions.Add(item);

            //  C2

            //  1
            item = new Production();
            item.AddCondition(new Fact("C2", "==", 0));
            item.AddFact(new Fact("C2:1", "==", 1));

            this._Productions.Add(item);

            //  2
            item = new Production();
            item.AddCondition(new Fact("C2", ">=", 0));
            item.AddCondition(new Fact("C2", "<=", 1));
            item.AddFact(new Fact("C2:2", "==", 1));

            this._Productions.Add(item);

            //  3, 9
            item = new Production();
            item.AddCondition(new Fact("C2", ">=", 0));
            item.AddCondition(new Fact("C2", "<=", 3));
            item.AddFact(new Fact("C2:3,9", "==", 1));

            this._Productions.Add(item);

            //  4
            item = new Production();
            item.AddCondition(new Fact("C2", ">=", 2));
            item.AddCondition(new Fact("C2", "<=", 3));
            item.AddFact(new Fact("C2:4", "==", 1));

            this._Productions.Add(item);

            //  5, 6, 7, 8, 0
            item = new Production();
            item.AddCondition(new Fact("C2", ">=", 0));
            item.AddCondition(new Fact("C2", "<=", 2));
            item.AddFact(new Fact("C2:5,6,7,8,0", "==", 1));

            this._Productions.Add(item);

            //  A3

            //  1
            item = new Production();
            item.AddCondition(new Fact("A3", ">=", 1));
            item.AddCondition(new Fact("A3", "<=", 2));
            item.AddFact(new Fact("A3:1", "==", 1));

            this._Productions.Add(item);

            //  2, 0
            item = new Production();
            item.AddCondition(new Fact("A3", ">=", 0));
            item.AddCondition(new Fact("A3", "<=", 3));
            item.AddFact(new Fact("A3:2,0", "==", 1));

            this._Productions.Add(item);

            //  3, 5, 6, 7, 8, 9
            item = new Production();
            item.AddCondition(new Fact("A3", ">=", 0));
            item.AddCondition(new Fact("A3", "<=", 2));
            item.AddFact(new Fact("A3:3,5,6,7,8,9", "==", 1));

            this._Productions.Add(item);

            //  4
            item = new Production();
            item.AddCondition(new Fact("A3", "==", 0));
            item.AddFact(new Fact("A3:4", "==", 1));

            this._Productions.Add(item);

            //  B3

            //  1
            item = new Production();
            item.AddCondition(new Fact("B3", "==", 3));
            item.AddFact(new Fact("B3:1", "==", 1));

            this._Productions.Add(item);

            //  2, 3, 0
            item = new Production();
            item.AddCondition(new Fact("B3", ">=", 2));
            item.AddCondition(new Fact("B3", "<=", 3));
            item.AddFact(new Fact("B3:2,3,0", "==", 1));

            this._Productions.Add(item);

            //  4
            item = new Production();
            item.AddCondition(new Fact("B3", "==", 0));
            item.AddFact(new Fact("B3:4", "==", 1));

            this._Productions.Add(item);

            //  5, 6, 7, 8
            item = new Production();
            item.AddCondition(new Fact("B3", "==", 2));
            item.AddFact(new Fact("B3:5,6,7,8", "==", 1));

            this._Productions.Add(item);

            //  9
            item = new Production();
            item.AddCondition(new Fact("B3", ">=", 0));
            item.AddCondition(new Fact("B3", "<=", 2));
            item.AddFact(new Fact("B3:9", "==", 1));

            this._Productions.Add(item);

            //  C3

            //  1
            item = new Production();
            item.AddCondition(new Fact("C3", ">=", 1));
            item.AddCondition(new Fact("C3", "<=", 2));
            item.AddFact(new Fact("C3:1", "==", 1));

            this._Productions.Add(item);

            //  2, 5, 6, 8, 0
            item = new Production();
            item.AddCondition(new Fact("C3", ">=", 0));
            item.AddCondition(new Fact("C3", "<=", 2));
            item.AddFact(new Fact("C3:2,5,6,8,0", "==", 1));

            this._Productions.Add(item);

            //  3
            item = new Production();
            item.AddCondition(new Fact("C3", ">=", 0));
            item.AddCondition(new Fact("C3", "<=", 3));
            item.AddFact(new Fact("C3:3", "==", 1));

            this._Productions.Add(item);

            //  4
            item = new Production();
            item.AddCondition(new Fact("C3", "==", 2));
            item.AddFact(new Fact("C3:4", "==", 1));

            this._Productions.Add(item);

            //  7, 9
            item = new Production();
            item.AddCondition(new Fact("C3", "==", 0));
            item.AddFact(new Fact("C3:7,9", "==", 1));

            this._Productions.Add(item);

            //  Numbers

            //  1
            item = new Production();
            item.AddCondition(new Fact("A1:1,4", "==", 1));
            item.AddCondition(new Fact("B1:1,5,7,0", "==", 1));
            item.AddCondition(new Fact("C1:1", "==", 1));
            item.AddCondition(new Fact("A2:1", "==", 1));
            item.AddCondition(new Fact("B2:1,4,5,8", "==", 1));
            item.AddCondition(new Fact("C2:1", "==", 1));
            item.AddCondition(new Fact("A3:1", "==", 1));
            item.AddCondition(new Fact("B3:1", "==", 1));
            item.AddCondition(new Fact("C3:1", "==", 1));
            item.AddFact(new Fact("Number", "==", 1));

            this._Productions.Add(item);

            //  2
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:2,6,8,9", "==", 1));
            item.AddCondition(new Fact("C1:2,4", "==", 1));
            item.AddCondition(new Fact("A2:2,9", "==", 1));
            item.AddCondition(new Fact("B2:2", "==", 1));
            item.AddCondition(new Fact("C2:2", "==", 1));
            item.AddCondition(new Fact("A3:2,0", "==", 1));
            item.AddCondition(new Fact("B3:2,3,0", "==", 1));
            item.AddCondition(new Fact("C3:2,5,6,8,0", "==", 1));
            item.AddFact(new Fact("Number", "==", 2));

            this._Productions.Add(item);

            //  3
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:3", "==", 1));
            item.AddCondition(new Fact("C1:3,7,0", "==", 1));
            item.AddCondition(new Fact("A2:3,5,8,0", "==", 1));
            item.AddCondition(new Fact("B2:3,9", "==", 1));
            item.AddCondition(new Fact("C2:3,9", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:2,3,0", "==", 1));
            item.AddCondition(new Fact("C3:3", "==", 1));
            item.AddFact(new Fact("Number", "==", 3));

            this._Productions.Add(item);
            //  4
            item = new Production();
            item.AddCondition(new Fact("A1:1,4", "==", 1));
            item.AddCondition(new Fact("B1:4", "==", 1));
            item.AddCondition(new Fact("C1:2,4", "==", 1));
            item.AddCondition(new Fact("A2:4", "==", 1));
            item.AddCondition(new Fact("B2:1,4,5,8", "==", 1));
            item.AddCondition(new Fact("C2:4", "==", 1));
            item.AddCondition(new Fact("A3:4", "==", 1));
            item.AddCondition(new Fact("B3:4", "==", 1));
            item.AddCondition(new Fact("C3:4", "==", 1));
            item.AddFact(new Fact("Number", "==", 4));

            this._Productions.Add(item);
            //  5
            item = new Production();
            item.AddCondition(new Fact("A1:5,0", "==", 1));
            item.AddCondition(new Fact("B1:1,5,7,0", "==", 1));
            item.AddCondition(new Fact("C1:5", "==", 1));
            item.AddCondition(new Fact("A2:3,5,8,0", "==", 1));
            item.AddCondition(new Fact("B2:1,4,5,8", "==", 1));
            item.AddCondition(new Fact("C2:5,6,7,8,0", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:5,6,7,8", "==", 1));
            item.AddCondition(new Fact("C3:2,5,6,8,0", "==", 1));
            item.AddFact(new Fact("Number", "==", 5));

            this._Productions.Add(item);
            //  6
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:2,6,8,9", "==", 1));
            item.AddCondition(new Fact("C1:6,8,9", "==", 1));
            item.AddCondition(new Fact("A2:6", "==", 1));
            item.AddCondition(new Fact("B2:6", "==", 1));
            item.AddCondition(new Fact("C2:5,6,7,8,0", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:5,6,7,8", "==", 1));
            item.AddCondition(new Fact("C3:2,5,6,8,0", "==", 1));
            item.AddFact(new Fact("Number", "==", 6));

            this._Productions.Add(item);
            //  7
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:1,5,7,0", "==", 1));
            item.AddCondition(new Fact("C1:3,7,0", "==", 1));
            item.AddCondition(new Fact("A2:7", "==", 1));
            item.AddCondition(new Fact("B2:7,0", "==", 1));
            item.AddCondition(new Fact("C2:5,6,7,8,0", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:5,6,7,8", "==", 1));
            item.AddCondition(new Fact("C3:7,9", "==", 1));
            item.AddFact(new Fact("Number", "==", 7));

            this._Productions.Add(item);
            //  8
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:2,6,8,9", "==", 1));
            item.AddCondition(new Fact("C1:6,8,9", "==", 1));
            item.AddCondition(new Fact("A2:3,5,8,0", "==", 1));
            item.AddCondition(new Fact("B2:1,4,5,8", "==", 1));
            item.AddCondition(new Fact("C2:5,6,7,8,0", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:5,6,7,8", "==", 1));
            item.AddCondition(new Fact("C3:2,5,6,8,0", "==", 1));
            item.AddFact(new Fact("Number", "==", 8));

            this._Productions.Add(item);
            //  9
            item = new Production();
            item.AddCondition(new Fact("A1:2,3,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B1:2,6,8,9", "==", 1));
            item.AddCondition(new Fact("C1:6,8,9", "==", 1));
            item.AddCondition(new Fact("A2:2,9", "==", 1));
            item.AddCondition(new Fact("B2:3,9", "==", 1));
            item.AddCondition(new Fact("C2:3,9", "==", 1));
            item.AddCondition(new Fact("A3:3,5,6,7,8,9", "==", 1));
            item.AddCondition(new Fact("B3:9", "==", 1));
            item.AddCondition(new Fact("C3:7,9", "==", 1));
            item.AddFact(new Fact("Number", "==", 9));

            this._Productions.Add(item);
            //  0
            item = new Production();
            item.AddCondition(new Fact("A1:5,0", "==", 1));
            item.AddCondition(new Fact("B1:1,5,7,0", "==", 1));
            item.AddCondition(new Fact("C1:3,7,0", "==", 1));
            item.AddCondition(new Fact("A2:3,5,8,0", "==", 1));
            item.AddCondition(new Fact("B2:7,0", "==", 1));
            item.AddCondition(new Fact("C2:5,6,7,8,0", "==", 1));
            item.AddCondition(new Fact("A3:2,0", "==", 1));
            item.AddCondition(new Fact("B3:2,3,0", "==", 1));
            item.AddCondition(new Fact("C3:2,5,6,8,0", "==", 1));
            item.AddFact(new Fact("Number", "==", 0));

            this._Productions.Add(item);
        }
    }
}
