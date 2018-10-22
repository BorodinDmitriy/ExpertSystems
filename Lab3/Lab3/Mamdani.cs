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

        public void Run(double HAR, double HDR, double EAR, double EDR)
        {
            List<double> heroAR = new List<double>();
            List<double> heroDR = new List<double>();
            this.Fuzzification(HAR, ref heroAR, HDR, ref heroDR);

            List<double> enemyAR = new List<double>();
            List<double> enemyDR = new List<double>();
            this.Fuzzification(EAR, ref enemyAR, EDR, ref enemyDR);

            this.Aggregation();

            return;
        }

        private List<Rule> Rules;

        private void Fuzzification(double AR, ref List<double> fuzzyAR, double DR, ref List<double> fuzzyDR)
        {
            //  AR => fuzzyAR ['Missing', 'Basic', 'Advance', 'Expert'];
            fuzzyAR.Add(MissingAR.GetValue(AR));
            fuzzyAR.Add(BasicAR.GetValue(AR));
            fuzzyAR.Add(AdvanceAR.GetValue(AR));
            fuzzyAR.Add(ExpertAR.GetValue(AR));

            //  DR => fuzzyDR ['Missing', 'Basic', 'Advance', 'Expert'];
            fuzzyDR.Add(MissingDR.GetValue(DR));
            fuzzyDR.Add(BasicDR.GetValue(DR));
            fuzzyDR.Add(AdvanceDR.GetValue(DR));
            fuzzyDR.Add(ExpertDR.GetValue(DR));
            return;
        }

        private void Aggregation()
        {

        }


        //  "AR" FussySet
        private TriangleSet MissingAR = new TriangleSet(new Point(0, 0), new Point(0, 1), new Point(1, 0));
        private TrapezeSet BasicAR = new TrapezeSet(new Point(0, 0), new Point(1, 1), new Point(30, 1), new Point(32, 0));
        private TrapezeSet AdvanceAR = new TrapezeSet(new Point(31, 0), new Point(32, 1), new Point(71, 1), new Point(73, 0));
        private TrapezeSet ExpertAR = new TrapezeSet(new Point(72, 0), new Point(74, 1), new Point(100, 1), new Point(100, 0));

        //  "DR" FuzzySet
        private TriangleSet MissingDR = new TriangleSet(new Point(0, 0), new Point(0, 1), new Point(3, 0));
        private TrapezeSet BasicDR = new TrapezeSet(new Point(1, 0), new Point(4, 1), new Point(32, 1), new Point(36, 0));
        private TrapezeSet AdvanceDR = new TrapezeSet(new Point(34, 0), new Point(38, 1), new Point(80, 1), new Point(85, 0));
        private TrapezeSet ExpertDR = new TrapezeSet(new Point(75, 0), new Point(90, 1), new Point(100, 1), new Point(100, 0));

        //  "D" FuzzySet
        private TriangleSet No = new TriangleSet(new Point(0, 0), new Point(0, 1), new Point(1, 0));
        private TriangleSet Low = new TriangleSet(new Point(0, 0), new Point(2, 1), new Point(15, 0));
        private TrapezeSet Minor = new TrapezeSet(new Point(10, 0), new Point(17, 1), new Point(38, 1), new Point(50, 0));
        private TrapezeSet Average = new TrapezeSet(new Point(40, 0), new Point(49, 1), new Point(75, 1), new Point(80, 0));
        private TrapezeSet High = new TrapezeSet(new Point(78, 0), new Point(89, 1), new Point(100, 1), new Point(100, 0));
        private void InitListRules()
        {
            //  AR - Atack Rate
            //  DR - Defence Rate
            //  D - Damage ["No","Low", "Minor", "Average", High]
            Rule temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Missing") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", MissingAR));
            temp.Conditions.Add(new Condition("DR", MissingDR));

            temp.Conclusions.Add(new Conclusion("D", Low));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Missing") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", BasicAR));
            temp.Conditions.Add(new Condition("DR", MissingDR));

            temp.Conclusions.Add(new Conclusion("D", Minor));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Missing") then ("D" is "Average")
            temp.Conditions.Add(new Condition("AR", AdvanceAR));
            temp.Conditions.Add(new Condition("DR", MissingDR));

            temp.Conclusions.Add(new Conclusion("D", Average));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Missing") then ("D" is "High")
            temp.Conditions.Add(new Condition("AR", ExpertAR));
            temp.Conditions.Add(new Condition("DR", MissingDR));

            temp.Conclusions.Add(new Conclusion("D", High));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Basic") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", MissingAR));
            temp.Conditions.Add(new Condition("DR", BasicDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Basic") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", BasicAR));
            temp.Conditions.Add(new Condition("DR", BasicDR));

            temp.Conclusions.Add(new Conclusion("D", Low));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Basic") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", AdvanceAR));
            temp.Conditions.Add(new Condition("DR", BasicDR));

            temp.Conclusions.Add(new Conclusion("D", Minor));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Basic") then ("D" is "Average")
            temp.Conditions.Add(new Condition("AR", ExpertAR));
            temp.Conditions.Add(new Condition("DR", BasicDR));

            temp.Conclusions.Add(new Conclusion("D", Average));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Advance") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", MissingAR));
            temp.Conditions.Add(new Condition("DR", AdvanceDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Advance") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", BasicAR));
            temp.Conditions.Add(new Condition("DR", AdvanceDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Advance") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", AdvanceAR));
            temp.Conditions.Add(new Condition("DR", AdvanceDR));

            temp.Conclusions.Add(new Conclusion("D", Low));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Advance") then ("D" is "Minor")
            temp.Conditions.Add(new Condition("AR", ExpertAR));
            temp.Conditions.Add(new Condition("DR", AdvanceDR));

            temp.Conclusions.Add(new Conclusion("D", Minor));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Missing" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", MissingAR));
            temp.Conditions.Add(new Condition("DR", ExpertDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Basic" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", BasicAR));
            temp.Conditions.Add(new Condition("DR", ExpertDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Advance" and "DR" is "Expert") then ("D" is "No")
            temp.Conditions.Add(new Condition("AR", AdvanceAR));
            temp.Conditions.Add(new Condition("DR", ExpertDR));

            temp.Conclusions.Add(new Conclusion("D", No));
            this.Rules.Add(temp);
            temp = new Rule();

            //  if ("AR" is "Expert" and "DR" is "Expert") then ("D" is "Low")
            temp.Conditions.Add(new Condition("AR", ExpertAR));
            temp.Conditions.Add(new Condition("DR", ExpertDR));

            temp.Conclusions.Add(new Conclusion("D", Low));
            this.Rules.Add(temp);
            temp = new Rule();
        }
    }
}
