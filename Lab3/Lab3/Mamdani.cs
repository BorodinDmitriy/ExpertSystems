using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Mamdani
    {
        public Mamdani()
        {
            this.Rules = new List<Rule>();
            InitListRules();
        }

        public void Run(HeroInfo player, HeroInfo enemy)
        {
            this.Fuzzification(player);
            this.Fuzzification(enemy);

            this.Aggregation();
        }

        private List<Rule> Rules;

        private void Fuzzification(HeroInfo user)
        {

        }

        private void Aggregation()
        {

        }

        private void InitListRules()
        {
            //  AR - Atack Rate
            //  DR - Defence Rate
            //  D - Damage
            Rule temp = new Rule();
            Condition condition = new Condition();
            Conclusion conclusion = new Conclusion();
            condition.Variable.Id = "AR";
            //condition.Term = "Missing";
            temp.Conditions.Add(condition);
            condition = new Condition();
            condition.Variable.Id = "DR";
            //condition.Term = "Missing";
            temp.Conditions.Add(condition);
            conclusion.Variable.Id = "D";

        }
    }
}
