using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Increment = 0.1m;
            numericUpDown2.Increment = 0.1m;
            numericUpDown3.Increment = 0.1m;
            numericUpDown4.Increment = 0.1m;
            comboBox1.SelectedIndex = 0;
            //FuzzyController.RunMamdani(fakePlayer, fakeEnemy);
            //FuzzyController.RunSugeno(fakePlayer, fakeEnemy);
        }

        private Controller FuzzyController = new Controller();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HeroInfo player = new HeroInfo(0, 0, 0, (double)numericUpDown1.Value, (double)numericUpDown3.Value);
            HeroInfo enemy = new HeroInfo(0, 0, 0, (double)numericUpDown2.Value, (double)numericUpDown4.Value);

            if (comboBox1.SelectedIndex == 0)
            {
                List<double> result = FuzzyController.RunMamdani(player, enemy);
                label6.Text = "Урон героя: " + result.ElementAt(0).ToString();
                label7.Text = "Урон врага: " + result.ElementAt(1).ToString();
            }
            else
            {
                List<double> result = FuzzyController.RunSugeno(player, enemy);
                label6.Text = "Урон героя: " + result.ElementAt(0).ToString();
                label7.Text = "Урон врага: " + result.ElementAt(1).ToString();
            }
        }
       // private HeroInfo fakePlayer = new HeroInfo(10, 10, 10, 50, 35);
        //private HeroInfo fakeEnemy = new HeroInfo(10, 10, 10, 72.5, 85);
        
    }
}
