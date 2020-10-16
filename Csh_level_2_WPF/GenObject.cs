using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Csh_level_2_WPF
{
    abstract class GenObject
    {
        protected string name;
        protected string code;
        protected double p_max;
        protected double p_min;

        protected GenObject(string name, string code) : this(name, code, 0, 0)
        { }

        internal GenObject(string name, string code, double Pmax, double Pmin)
        {
            this.name = name;
            this.code = code;
            try
            {
                if (Pmax < Pmin) throw new ArgumentException();

                p_max = Pmax;
                p_min = Pmin;

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Значение Рмин не может быть больше Рмакс");
            }
        }
        abstract internal Dictionary<string, string> ParamNames();
    }


}
