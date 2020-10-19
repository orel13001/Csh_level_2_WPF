using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_level_2_WPF
{
    class Station : GenObject
    {
        internal List<GTP> gtps = new List<GTP>();
        public Station(int id, string name, string code) : base(id, name, code)
        {            
        }

        public bool AddGTP(GTP gtp)
        {
            var names = (from _gtp in gtps
                         select _gtp.Name).ToList();
            var codes = (from _gtp in gtps
                         select _gtp.Code).ToList();
            if (!codes.Contains(gtp.Code) && !names.Contains(gtp.Name))
            {
                gtps.Add(gtp);
                return true;
            }
            return false;
        }

        public bool RemoveGTP(GTP gtp)
        {
            var names = (from _gtp in gtps
                         select _gtp.Name).ToList();
            var codes = (from _gtp in gtps
                         select _gtp.Code).ToList();
            if (codes.Contains(gtp.Code) || names.Contains(gtp.Name))
            {
                gtps.Remove(gtp);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return this.name;
        }


        public string Name { get => name; }
        public string Code { get => code; }
        public double Pmax { get => (from _gtp in gtps select _gtp.Pmax).ToList().Sum(); }
        public double Pmin { get => (from _gtp in gtps select _gtp.Pmin).ToList().Sum(); }
        public int CountGTP { get => gtps.Count; }

        internal override Dictionary<string,string> ParamNames()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("Наименование", Name);
            param.Add("Код", Code);
            param.Add("Рмакс", Convert.ToString(Pmax));
            param.Add("Рмин", Convert.ToString(Pmin));
            param.Add("Число ГТП", Convert.ToString(CountGTP));

            return param;
        }

        internal static bool AddStstionToList(Station station,  ICollection<Station> stations)
        {
            var names = (from _st in stations
                         select _st.Name).ToList();
            var codes = (from _st in stations
                         select _st.Code).ToList();
            if (!codes.Contains(station.Code) && !names.Contains(station.Name))
            {
                stations.Add(station);
                return true;
            }
            return false;
        }

        internal static bool DelStationFromList(Station station, ICollection<Station> stations)
        {
            if (stations.Contains(station))
            {
                stations.Remove(station);
                return true;
            }
            return false;
        }
    }
}
