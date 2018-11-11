using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroFuzzy;
using NeuroFuzzy.membership;
using NeuroFuzzy.misc;
using NeuroFuzzy.training;
using NeuroFuzzy.rextractors;


namespace NeuroFuzzy
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Инициализация обучающей выборки");
            List<double[]> x = new List<double[]>();
            List<double[]> y = new List<double[]>();

            double ar;
            double dr;
            for (double i = 0; i <= 100; i+= 1)
            {
                for (double j = 0; j<= 100; j+= 1)
                {
                    ar = i;
                    dr = j;
                    //  high 100%
                    if (ar >= 80 && ar <= 100 && dr >= 0 && dr <= 1)
                    {
                        x.Add(new double[] { ar, dr });
                        y.Add(new double[] { 0, 0, 0, 0, 1 });
                    }
                    //  average 100%
                    else if ((ar >= 31 && ar <= 70 && dr >= 0 && dr <= 1) || (ar >= 80 && ar <= 100 && dr > 1 && dr <= 30))
                    {
                        x.Add(new double[] { ar, dr });
                        y.Add(new double[] { 0, 0, 0, 1, 0 });
                    }
                    //  minor 100%
                    else if ((ar >= 4 && ar <= 29 && dr >= 0 && dr <= 1) || (ar >= 31 && ar <= 70 && dr > 1 && dr <= 30) || (ar >= 80 && ar <= 100 && dr >= 40 && dr <= 70))
                    {
                        x.Add(new double[] { ar, dr });
                        y.Add(new double[] { 0, 0, 1, 0, 0 });
                    }
                    //  small 100%
                    else if ((ar >= 0 && ar <= 1 && dr >= 0 && dr <= 1) || (ar >= 4 && ar <= 29 && dr > 1 && dr <= 30) || (ar >= 31 && ar <= 70 && dr >= 40 && dr <= 70) || (ar >= 80 && ar <= 100 && dr >= 90 && dr <= 100))
                    {
                        x.Add(new double[] { ar, dr });
                        y.Add(new double[] { 0, 1, 0, 0, 0 });
                    }
                    //  No 100%
                    else if ((ar >= 0 && ar <= 1 && dr> 4 && dr<= 100)|| (ar >= 1 && ar <= 25 && dr >= 30 && dr <= 100) || (ar >= 25 && ar <= 45 && dr >= 60 && dr <= 100) ||(ar >= 45 && ar <= 70 && dr >= 90 & dr <= 100))
                    {
                        x.Add(new double[] { ar, dr });
                        y.Add(new double[] { 1, 0, 0, 0, 0 });
                    }
                }
            }

            Backprop bprop = new Backprop(1e-3);

            СMEANSExtractorIO extractor = new СMEANSExtractorIO(5);

            Console.WriteLine("Инициализация ANFIS");
            ANFIS fis = ANFISBuilder<GaussianRule>.Build(x.ToArray(), y.ToArray(), extractor, bprop, 1000);

            bool state = true;
            while(state)
            {
                Console.Write("Введите коэффициент атаки: ");
                string ars = Console.ReadLine();
                if (ars == "q" || ars == "quit" || ars == "Q" || ars == "Quit")
                {
                    state = false;
                    continue;
                }
                ar = Convert.ToDouble(ars);
                Console.Write("Введите коэффициент защиты: ");
                string drs = Console.ReadLine();
                if (drs == "q" || drs == "quit" || drs == "Q" || drs == "Quit")
                {
                    state = false;
                    continue;
                }
                dr = Convert.ToDouble(drs);

                double[] args = new double[] { ar, dr };
                double[] res = fis.Inference(args);

                int maxI = 0;
                double max = res[0];
                for (int I = 1; I < res.Length; I++)
                {
                    if (max < res[I])
                    {
                        maxI = I;
                        max = res[I];
                    }
                }

                switch(maxI)
                {
                    case 0:
                        Console.WriteLine("Вероятнее всего урона не будет");
                        break;
                    case 1:
                        Console.WriteLine("Вероятнее всего урон будет очень маленьким");
                        break;
                    case 2:
                        Console.WriteLine("Вероятнее всего урон будет незначительным");
                        break;
                    case 3:
                        Console.WriteLine("Вероятнее всего урон будет средним");
                        break;
                    case 4:
                        Console.WriteLine("Вероятнее всего урон будет большим");
                        break;
                }


            }
            Console.ReadKey();
        }
    }
}
