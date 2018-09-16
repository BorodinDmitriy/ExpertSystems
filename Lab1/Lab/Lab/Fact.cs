using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class Fact
    {
        private string _Name;
        private int _Value;

        public Fact(string name, int value)
        {
            this._Name = name;
            this._Value = value;
        }

        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                this._Name = value;
            }
        }

        public int Value
        {
            get
            {
                return this._Value;
            }

            set
            {
                this._Value = value;
            }
        }
    }
}
