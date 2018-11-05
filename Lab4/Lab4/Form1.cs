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
        public Form1()
        {
            InitializeComponent();
            Test();
            List<List<double>> matrix = controller.Calculate();
            double K = 0;
            K = matrix[0][0];
            
        }

        private Controller controller = new Controller();

        private void Test()
        {
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
                for (int J = 0; J <= 1 * (I - 31); J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 72; I <= 100; I++)
            {
                for(int J = 5; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            //  Minor

            for (int I = 2; I <= 30; I++)
            {
                for (int J = 0; J <= 2; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 32; I <= 71; I++)
            {
                for (int J = 4; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 74; I <= 100; I++)
            {
                for (int J = 36; J <= 75; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            //  Low
            for (int I = 0; I <= 1; I++) 
            {
                for (int J = 0; J <= 2; J++) 
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 1; I <= 30; I++)
            {
                for (int J = 4; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 32; I <= 71; I++)
            {
                for (int J = 38; J <= 75; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 76; I <= 100; I++)
            {
                for (int J = 90; J <=100; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

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
                for (int J = 38; J <= 80; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }

            for (int I = 0; I <= 1; I++)
            {
                for (int J = 4; J <= 32; J++)
                {
                    controller.Add(new Point(I, J));
                }
            }
            
        }
    }
}
