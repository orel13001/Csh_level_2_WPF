using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Csh_level_2_WPF
{
    class ChangeObject
    {

        internal static void MoveEGO(WinChange winChange)
        {
            try
            {
                Station station = MainWindow.stations[winChange.cbStation.SelectedIndex];
                GTP gtp = station.gtps[winChange.cbGTP.SelectedIndex];
                GTP gtp_new = station.gtps[winChange.cbGTPnew.SelectedIndex];
                EGO ego = gtp.egos[winChange.cbEGO.SelectedIndex];

                if (gtp_new.AddEGO(ego) && gtp.RemoveEGO(ego))
                    MessageBox.Show($"ЕГО {ego.Name} перенесена в {gtp_new.Name}");
                else
                    MessageBox.Show($"перенос ЕГО {ego.Name} завершился неудачей");

            }
            catch (Exception)
            {
                MessageBox.Show("Проверте корректность заполнения полей");
            }
        }
    


        internal static void DelStation(WinChange winChange)
        {
            try
            {
                Station station = MainWindow.stations[winChange.cbStation.SelectedIndex];
                if (Station.DelStationFromList(station, (ICollection<Station>)MainWindow.stations)) 
                    MessageBox.Show($"Электростанция {MainWindow.stations[winChange.cbStation.SelectedIndex]} удалена");
                else
                    MessageBox.Show($"Электростанция {MainWindow.stations[winChange.cbStation.SelectedIndex]} отсутствует");
            }
            catch (Exception)
            {
                MessageBox.Show("Проверте корректность заполнения полей");
            }
        }


        internal static void DelGTP(WinChange winChange)
        {
            try
            {
                Station station = MainWindow.stations[winChange.cbStation.SelectedIndex];
                GTP gtp = station.gtps[winChange.cbGTP.SelectedIndex];
                if (station.RemoveGTP(gtp))
                    MessageBox.Show($"ГТП {gtp.Name} удалена");
                else
                    MessageBox.Show($"ГТП {gtp.Name} отсутствует");
            }
            catch (Exception)
            {
                MessageBox.Show("Проверте корректность заполнения полей");
            }
        }


        internal static void DelEGO(WinChange winChange)
        {
            try
            {
                Station station = MainWindow.stations[winChange.cbStation.SelectedIndex];
                GTP gtp = station.gtps[winChange.cbGTP.SelectedIndex];
                EGO ego = gtp.egos[winChange.cbEGO.SelectedIndex];

                if (gtp.RemoveEGO(ego))
                    MessageBox.Show($"ЕГО {ego.Name} удалена");
                else
                    MessageBox.Show($"ЕГО {ego.Name} отсутствует");

            }
            catch (Exception)
            {
                MessageBox.Show("Проверте корректность заполнения полей");
            }
        }

    }
}
