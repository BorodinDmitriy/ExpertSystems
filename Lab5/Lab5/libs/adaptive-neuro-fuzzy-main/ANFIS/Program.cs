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
            int trainingSamples = 2000;
            double[][] x = new double[trainingSamples][];
            double[][] y = new double[trainingSamples][];

            double px = 0.1;
            double r = 3.8;
            double lx = r * px * (1 - px);

            for (int i = 0; i < trainingSamples; i++)
            {
                x[i] = new double[] { px, lx };
                px = lx;
                lx = r * lx * (1 - lx);
                y[i] = new double[] { lx };
            }

            Backprop bprop = new Backprop(1e-2);

            KMEANSExtractorIO extractor = new KMEANSExtractorIO(10);

            ANFIS fis = ANFISBuilder<GaussianRule>.Build(x, y, extractor, bprop, 1000);


            double[] arg = new double[2] { 0.2, 0.25 };

            double[] res = fis.Inference(arg);

            Console.WriteLine("A:" + x[10][0] + " B:" + x[10][1] + " Y:" + y[10][0]);
            Console.WriteLine(res[0]);
            Console.ReadKey();
        }
    }
}
