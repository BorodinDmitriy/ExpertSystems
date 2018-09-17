using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class Condition
    {

        public Condition(string name, int min, int max)
        {
            this._Name = name;
            this._Min = min;
            this._Max = max;
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

        public int Min
        {
            get
            {
                return this._Min;
            }

            set
            {
                this._Min = value;
            }
        }

        public int Max
        {
            get
            {
                return this._Max;
            }

            set
            {
                this._Max = value;
            }
        }

        public bool CheckCondition(Fact fact)
        {
            bool result = false;
            if (fact.Name != this._Name)
                return result;

            if (fact.Value >= this._Min && fact.Value <= this._Max)
            {
                return true;
            }
            return result;
        }       

        private string _Name;
        private int _Min;
        private int _Max;


        
    }
}
