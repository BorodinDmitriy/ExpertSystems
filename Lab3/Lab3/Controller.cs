using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public struct HeroInfo
    {
        public HeroInfo(int level, int atack, int defence, int atackRate, int defenceRate)
        {
            this.Level = level;
            this.Atack = atack;
            this.Defence = defence;
            this.AtackRate = atackRate;
            this.DefenceRate = defenceRate;
        }

        public int Level;
        public int Atack;
        public int Defence;
        public int AtackRate;
        public int DefenceRate;
    }
    class Controller
    {
        public void RunMamdani(HeroInfo player, HeroInfo enemy)
        {
            Mamdani.Run(player, enemy);
        }

        private Mamdani Mamdani = new Mamdani();
    }
}
