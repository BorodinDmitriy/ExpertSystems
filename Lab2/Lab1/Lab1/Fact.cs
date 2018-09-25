using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Fact
    {

        public Fact(string name, string predicat, int value)
        {
            this._Name = name;
            this._Predicat = predicat;
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

        public string Predicat
        {
            get
            {
                return this._Predicat;
            }

            set
            {
                this._Predicat = value;
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

        public bool CheckCondition(Fact fact)
        {
            bool result = false;
            if (fact.Name != this._Name)
                return result;

            switch (this._Predicat)
            {
                case "==":
                    if (this._Value == fact._Value)
                        return true;
                    break;
                case ">=":
                    if (fact.Value >= this._Value)
                        return true;
                    break;
                case "<=":
                    if (fact.Value <= this._Value)
                        return true;
                    break;
            }
            return result;
        }       

        private string _Name;
        private int _Value;
        private string _Predicat;       
    }
}
