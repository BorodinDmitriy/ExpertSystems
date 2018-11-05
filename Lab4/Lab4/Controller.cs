using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Controller
    {
        private int C = 5;
        private double Eps = 10e-3;
        private List<Point> startCentroid = new List<Point>();
        private List<Point> vectorPoint = new List<Point>();
        private List<List<double>> matrix = new List<List<double>>();
        
        public Controller()
        {
            initClustering();
        }

        public void Add(Point point)
        {
            this.vectorPoint.Add(point);
            this.matrix.Add(new List<double>(this.C));
            for (int I = 0; I < this.C; I++)
            {
                this.matrix[this.matrix.Count - 1][I] = 0;
            }
            return;
        }

        public List<List<double>> Calculate()
        {
            bool state = false;
            List<double> distance = new List<double>(this.C);
            double prev = FindMaxU();
            double current = prev;
            while(state) 
            {
                prev = current;
                CorrectCenterOfCluster();
                RefreshMatrix();

                current = FindMaxU();
                if (Math.Abs(current - prev) < this.Eps)
                {
                    state = true;
                }
            }

            
            return matrix;
        }

        private void RefreshMatrix()
        {
            for (int I = 0; I < this.C; I++)
            {
                for (int K = 0; K < this.vectorPoint.Count; K++)
                {
                    double diveder = 0;
                    for (int J = 0; J < this.C; J++)
                    {
                        diveder += Math.Pow((Distance(this.vectorPoint[K], this.startCentroid[I]) / Distance(this.vectorPoint[K], this.startCentroid[J])), (2 / (this.vectorPoint.Count - 1)));
                    }

                    matrix[K][I] = 1 / diveder;
                }
            }
        }

        private double Distance(Point x, Point c)
        {
            double a = Math.Pow(x.AR - c.AR, 2);
            double b = Math.Pow(x.DR - c.DR, 2);
            double y = Math.Sqrt(a + b);
            double res = Math.Sqrt(Math.Pow(y, 2));
            return res;
        }

        private void CorrectCenterOfCluster()
        {
            for (int I = 0; I < this.startCentroid.Count; I++)
            {
                double x = 0;
                double y = 0;
                double del = 0;
                for (int K = 0; K < this.vectorPoint.Count; K++)
                {
                    x += (this.matrix[K][I] * this.vectorPoint[K].AR);
                    y += (this.matrix[K][I] * this.vectorPoint[K].DR);
                    del += this.matrix[K][I];
                }


                this.startCentroid[I].AR = x / del;
                this.startCentroid[I].DR = y / del; 
            }

            return;
        }

        private double FindMaxU()
        {
            double res = matrix[0][0];
            for (int K = 0; K < this.vectorPoint.Count; K++)
            {
                for (int J = 0; J < this.C; J++)
                {
                    if (res > matrix[K][J])
                    {
                        res = matrix[K][J];
                    }
                }
            }

            return res;
        }
        private void initClustering()
        {
            //  No
            this.startCentroid.Add(new Point(0.5, 1.5));
            this.vectorPoint.Add(new Point(0.5, 1.5));
            this.matrix.Add(new List<double>(C));
            this.matrix[0][0] = 1;
            //  Low
            this.startCentroid.Add(new Point(15, 13));
            this.vectorPoint.Add(new Point(0.5, 1.5));
            this.matrix.Add(new List<double>(C));
            this.matrix[1][1] = 1;
            //  Minor
            this.startCentroid.Add(new Point(55, 34));
            this.vectorPoint.Add(new Point(55, 34));
            this.matrix.Add(new List<double>(C));
            this.matrix[2][2] = 1;
            //  Average
            this.startCentroid.Add(new Point(65, 40));
            this.vectorPoint.Add(new Point(65, 40));
            this.matrix.Add(new List<double>(C));
            this.matrix[3][3] = 1;
            //  High
            this.startCentroid.Add(new Point(85, 1.5));
            this.vectorPoint.Add(new Point(85, 1.5));
            this.matrix.Add(new List<double>(C));
            this.matrix[4][4] = 1;

            for (int I = 0; I < this.C; I++)
            {
                for (int J = 0; J < this.C; J++)
                {
                    if (I != J)
                        this.matrix[I][J] = 0;
                }
            }
            return;
        }
    }
}
