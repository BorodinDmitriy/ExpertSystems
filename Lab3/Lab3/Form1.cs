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
            FuzzyController.RunMamdani(fakePlayer, fakeEnemy);
            FuzzyController.RunSugeno(fakePlayer, fakeEnemy);
        }

        private Controller FuzzyController = new Controller();
        private HeroInfo fakePlayer = new HeroInfo(10, 10, 10, 50, 35);
        private HeroInfo fakeEnemy = new HeroInfo(10, 10, 10, 72.5, 85);
        
    }
}
