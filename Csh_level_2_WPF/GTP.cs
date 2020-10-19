using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_level_2_WPF
{
    class GTP : GenObject
    {
        internal List<EGO> egos = new List<EGO>();
        public GTP(int id, string name, string code) : base(id, name, code)
        {
        }

        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
        public double Pmin { get => (from _ego in egos select _ego.Pmin).ToList().Sum();  }
        public double Pmax { get => (from _ego in egos select _ego.Pmax).ToList().Sum();  }

        public bool AddEGO(EGO ego)
        {
            var names = (from _ego in egos
                         select _ego.Name).ToList();
            var codes = (from _gtp in egos
                         select _gtp.Code).ToList();
            if (!codes.Contains(ego.Code) && !names.Contains(ego.Name))
            {
                egos.Add(ego);
                return true;
            }
            return false;
        }

        public bool RemoveEGO(EGO ego)
        {
            var names = (from _ego in egos
                         select _ego.Name).ToList();
            var codes = (from _gtp in egos
                         select _gtp.Code).ToList();
            if (codes.Contains(ego.Code) || names.Contains(ego.Name))
            {
                egos.Remove(ego);
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return this.name;
        }

        public int CountEGO { get => egos.Count; }

        

        internal override Dictionary<string, string> ParamNames()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("Наименование", Name);
            param.Add("Код", Code);
            param.Add("Рмакс", Convert.ToString(Pmax));
            param.Add("Рмин", Convert.ToString(Pmin));
            param.Add("Число ЕГО", Convert.ToString(CountEGO));

            return param;
        }
    }
}
