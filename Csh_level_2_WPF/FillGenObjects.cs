using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_level_2_WPF
{
    class FillGenObjects
    {
        static Random rnd = new Random();
        public static ObservableCollection<Station> FillStation()
        {
            ObservableCollection<Station> stations = new ObservableCollection<Station>()
            {
                new Station("Томь-Усинская ГРЭС", "SKUZEN01"),
                new Station("Беловская ГРЭС", "SKUZEN02"),
                new Station("Назаровская ГРЭС", "SKUZEN03"),
                new Station("Приморская ГРЭС", "SKUZEN04"),
            };

            for (int s = 0; s < stations.Count; s++)
            {
                int gtpCount = rnd.Next(1, 5);
                for (int g = 0; g < gtpCount; g++)
                {
                    GTP gtp = new GTP($"ГТП-{g + 1}", $"GKUZEN{s}{g}");
                    
                    int egoCount = rnd.Next(1, 8);                    
                    for (int e = 0; e < egoCount; e++)
                    {
                        gtp.AddEGO(new EGO($"ЕГО-{s + 1}_{g + 1}_{e+1}", $"EKUZEN_{s}_{g}_{e}", rnd.Next(100, 200), rnd.Next(20, 80)));
                    }
                    stations[s].AddGTP(gtp);
                }
                
            }
            return stations;
        }        
    }
}
