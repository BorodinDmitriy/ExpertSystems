using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public struct HeroInfo
    {
        public HeroInfo(double level, double atack, double defence, double atackRate, double defenceRate)
        {
            this.Level = level;
            this.Atack = atack;
            this.Defence = defence;
            this.AtackRate = atackRate;
            this.DefenceRate = defenceRate;
        }

        public double Level;
        public double Atack;
        public double Defence;
        public double AtackRate;
        public double DefenceRate;
    }
    class Controller
    {
        public void RunMamdani(HeroInfo player, HeroInfo enemy)
        {
            Mamdani.Run(player.AtackRate, player.DefenceRate, enemy.AtackRate, enemy.DefenceRate);
        }

        private Mamdani Mamdani = new Mamdani();
    }
}
