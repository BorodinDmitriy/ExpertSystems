using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        int baseGraphPointX = 450;
        int baseGraphPointY = 320;
        int pointSize = 2;
        float centerPointValue;
        Graphics g;
        Pen p;
        double scaleX = 3.0;
        double scaleY = 3.0;

        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            p = new Pen(Color.Black, 1);
            centerPointValue = (float) pointSize / 2;
        }

        private Controller controller = new Controller();

        private void Test()
        {
            //MLApp.MLApp matlab = new MLApp.MLApp();
            //  High
            for (int I = 76; I <= 100; I++)
            {
                for (int J = 0; J <= 1; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }


            //  Average
            for (int I = 32; I <= 72; I++)
            {
                for (int J = 0; J <= 1 * ((I - 31) * 0.5); J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 72; I <= 100; I++)
            {
                for (int J = 5; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            //  Minor

            for (int I = 3; I <= 28; I++)
            {
                for (int J = 0; J <= 1 * (I - 2); J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 28; I <= 30; I++)
            {
                for (int J = 5; J <= 28 * (I - 27); J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 30; I <= 74; I++)
            {
                for (int J = 7; J <= 30 * Math.Max(((I - 29) / 0.5), 1); J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 74; I <= 100; I++)
            {
                for (int J = 36; J <= 70; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            ////  Low
            //for (int I = 0; I <= 1; I++) 
            //{
            //    for (int J = 0; J <= 2; J++) 
            //    {
            //        controller.Add(new Point(I, J));
            //    }
            //}

            //for (int I = 1; I <= 30; I++)
            //{
            //    for (int J = 4; J <= 32; J++)
            //    {
            //        controller.Add(new Point(I, J));
            //    }
            //}

            //for (int I = 32; I <= 71; I++)
            //{
            //    for (int J = 38; J <= 75; J++)
            //    {
            //        controller.Add(new Point(I, J));
            //    }
            //}

            //for (int I = 76; I <= 100; I++)
            //{
            //    for (int J = 90; J <=100; J++)
            //    {
            //        controller.Add(new Point(I, J));
            //    }
            //}

            //  No

            for (int I = 0; I <= 71; I++)
            {
                for (int J = 90; J <= 100; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 0; I <= 30; I++)
            {
                for (int J = 38 + I * 2; J <= 80; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 0; I <= 1; I++)
            {
                for (int J = 4 + I * 4; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

        }

        private void DrawGraph(Graphics g, Pen p, List<List<double>> mtr, List<Point> pts) {

            p.Color = Color.Black;
             // draw axis X and axis Y
            g.DrawLine(p, baseGraphPointX, baseGraphPointY - 310, baseGraphPointX, baseGraphPointY + 10);
            g.DrawLine(p, baseGraphPointX - 10, baseGraphPointY, baseGraphPointX + 310, baseGraphPointY);
            // sign axis 
            g.DrawString("DR", new Font("Times New Roman", (float)12.0), Brushes.Black, baseGraphPointX - 55, baseGraphPointY - 315);
            g.DrawString("AR", new Font("Times New Roman", (float)12.0), Brushes.Black, (float)(baseGraphPointX + 110 * scaleX - 10), baseGraphPointY );

            g.DrawString("0", new Font("Times New Roman", (float)12.0), Brushes.Black, baseGraphPointX - 25, baseGraphPointY +15);
            // draw little lines for values
            for (int i = 0; i <= 10; i++)
            {
                g.DrawLine(p, baseGraphPointX - 5, (float)(baseGraphPointY - i * 10 * scaleY), baseGraphPointX + 5, (float)(baseGraphPointY - i * 10 * scaleY));
                g.DrawLine(p, (float)(baseGraphPointX + i * 10 * scaleX), baseGraphPointY - 5, (float)(baseGraphPointX + i * 10 * scaleX), baseGraphPointY + 5);

                // draw signs near some little lines
                if ((i % 2 == 0) && (i > 0))
                {
                    g.DrawString((i * 10).ToString(), new Font("Times New Roman", (float)12.0), Brushes.Black, baseGraphPointX - 35, (float)(baseGraphPointY - i * 10 * scaleY - 10));
                    g.DrawString((i * 10).ToString(), new Font("Times New Roman", (float)12.0), Brushes.Black,  (float)(baseGraphPointX + i * 10 * scaleX - 10), baseGraphPointY + 15);
                }
            }

            // draw a list of points
            // pen types:
            // 0 - green
            // 1 - red
            // 2 - blue
            // 3 - yellow
            // 4 - black

            for (int i = 0; i < mtr.Count; i++)
            {
                int penType = 4;
                double maxVal = 0.0;
                for (int j = 0; j < mtr[i].Count; j++)
                {
                    if (mtr[i][j] > maxVal)
                    {
                        maxVal = mtr[i][j];
                        penType = j;
                    }
                }

                switch (penType)
                {
                    case 0:
                        p.Color = Color.Green;
                        break;
                    case 1:
                        p.Color = Color.Red;
                        break;
                    case 2:
                        p.Color = Color.Blue;
                        break;
                    case 3:
                        p.Color = Color.Yellow;
                        break;
                    case 4:
                        p.Color = Color.Black;
                        break;
                }
                if ((pts[i].AR <= 100) && (pts[i].DR <= 100) && (pts[i].AR >= 0) && (pts[i].DR >= 0) )
                {
                    g.DrawEllipse(p, (float)(baseGraphPointX + pts[i].AR * scaleX - centerPointValue), (float)(baseGraphPointY - pts[i].DR * scaleY - centerPointValue), pointSize, pointSize);
                }
                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
            List<List<double>> matrix = controller.Calculate();
            List<Point> points = controller.getVectorPoint();
            double K = 0;
            K = matrix[0][0];
            DrawGraph(g, p, matrix, points);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Form1.DefaultBackColor);
        }
    }
}
