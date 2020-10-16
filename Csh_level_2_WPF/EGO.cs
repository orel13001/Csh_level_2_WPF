using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Csh_level_2_WPF
{
    class EGO : GenObject
    {
        public EGO(string name, string code, double Pmax, double Pmin) : base(name, code, Pmax, Pmin)
        { }

        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
        public double Pmax
        {
            get => p_max;
            set
            {
                try
                {
                    if (value < p_min) throw new ArgumentException();
                    p_max = value;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Значение Рмин не может быть больше Рмакс");
                }

            }
        }
        public double Pmin
        {
            get => p_min;
            set
            {
                try
                {
                    if (value > p_max) throw new ArgumentException();
                    p_min = value;
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Значение Рмин не может быть больше Рмакс");
                }

            }
        }
        public override string ToString()
        {
            return this.name;
        }

        internal override Dictionary<string, string> ParamNames()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("Наименование", Name);
            param.Add("Код", Code);
            param.Add("Рмакс", Convert.ToString(Pmax));
            param.Add("Рмин", Convert.ToString(Pmin));

            return param;
        }
    }
}
