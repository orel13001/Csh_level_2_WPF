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

namespace Csh_level_2_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Station> stations;
        List<GTP> gtps;
        List<EGO> egos;


        public MainWindow()
        {
            InitializeComponent();

            stations = FillGenObjects.FillStation();

            cbStations.ItemsSource = stations;
            //cbGTPs.ItemsSource = stations[cbStations.SelectedIndex].gtps;

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

    }
}
