using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Csh_level_2_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static internal ObservableCollection<Station> stations;

        public MainWindow()
        {
            InitializeComponent();

            stations = FillFromDB.GetStationsFromDB();


            

            cbStations.ItemsSource = stations;

        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearElements();
            cbGTPs.ItemsSource = stations[cbStations.SelectedIndex].gtps;

            lvStation.ItemsSource = stations[cbStations.SelectedIndex].ParamNames();
        }

        private void cbGTPs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEGO.ItemsSource = null;
            if (cbGTPs.SelectedIndex != -1)
            {
                cbEGOs.ItemsSource = stations[cbStations.SelectedIndex].gtps[cbGTPs.SelectedIndex].egos;

                lvGTP.ItemsSource = stations[cbStations.SelectedIndex].gtps[cbGTPs.SelectedIndex].ParamNames();
            }
        }


        private void cbEGOs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEGOs.SelectedIndex != -1)
            {
                lvEGO.ItemsSource = stations[cbStations.SelectedIndex].gtps[cbGTPs.SelectedIndex].egos[cbEGOs.SelectedIndex].ParamNames();
            }
        }

        void ClearElements()
        {
            cbEGOs.ItemsSource = null;
            cbGTPs.ItemsSource = null;
            lvEGO.ItemsSource = null;
            lvGTP.ItemsSource = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinAdd winAdd = new WinAdd();
            winAdd.Show();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
        //    //WinChange winChange = new WinChange();
        //    //winChange.Show();
        }
    }
}
