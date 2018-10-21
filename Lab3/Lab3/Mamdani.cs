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
            //  D - Damage ["No","Low", "Minor", "Average", "High"]
            Rule temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Missing") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", "Missing"));
            temp.Conditions.Add(new Condition("DR", "Missing"));

            temp.Conclusions.Add(new Conclusion("D", "Low"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Missing") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", "Basic"));
            temp.Conditions.Add(new Condition("DR", "Missing"));

            temp.Conclusions.Add(new Conclusion("D", "Minor"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Missing") then ("D" is "Average")
            temp.Conditions.Add(new Condition("AR", "Advance"));
            temp.Conditions.Add(new Condition("DR", "Missing"));

            temp.Conclusions.Add(new Conclusion("D", "Average"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Missing") then ("D" is "High")
            temp.Conditions.Add(new Condition("AR", "Expert"));
            temp.Conditions.Add(new Condition("DR", "Missing"));

            temp.Conclusions.Add(new Conclusion("D", "High"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Basic") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Missing"));
            temp.Conditions.Add(new Condition("DR", "Basic"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Basic") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", "Basic"));
            temp.Conditions.Add(new Condition("DR", "Basic"));

            temp.Conclusions.Add(new Conclusion("D", "Low"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Basic") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", "Advance"));
            temp.Conditions.Add(new Condition("DR", "Basic"));

            temp.Conclusions.Add(new Conclusion("D", "Minor"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Basic") then ("D" is "Average")
            temp.Conditions.Add(new Condition("AR", "Expert"));
            temp.Conditions.Add(new Condition("DR", "Basic"));

            temp.Conclusions.Add(new Conclusion("D", "Average"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Advance") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Missing"));
            temp.Conditions.Add(new Condition("DR", "Advance"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Advance") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Basic"));
            temp.Conditions.Add(new Condition("DR", "Advance"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Advance") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", "Advance"));
            temp.Conditions.Add(new Condition("DR", "Advance"));

            temp.Conclusions.Add(new Conclusion("D", "Low"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Advance") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", "Expert"));
            temp.Conditions.Add(new Condition("DR", "Advance"));

            temp.Conclusions.Add(new Conclusion("D", "Minor"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Missing"));
            temp.Conditions.Add(new Condition("DR", "Expert"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Basic"));
            temp.Conditions.Add(new Condition("DR", "Expert"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", "Advance"));
            temp.Conditions.Add(new Condition("DR", "Expert"));

            temp.Conclusions.Add(new Conclusion("D", "No"));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Expert") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", "Expert"));
            temp.Conditions.Add(new Condition("DR", "Expert"));

            temp.Conclusions.Add(new Conclusion("D", "Low"));
            this.Rules.Add(temp);
            temp = new Rule();
        }
    }
}
