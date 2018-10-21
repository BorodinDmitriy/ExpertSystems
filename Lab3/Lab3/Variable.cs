using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Variable
    {
        public string Id
        {
            set { this.id = value; }
            get { return this.id; }
        }

        private string id;
    }
}
