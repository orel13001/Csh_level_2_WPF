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
using System.Windows.Shapes;

namespace Csh_level_2_WPF
{
    /// <summary>
    /// Логика взаимодействия для WinAdd.xaml
    /// </summary>
    public partial class WinAdd : Window
    {
        private List<string> type_obj = new List<string>() { "Электростанция", "ГТП", "ЕГО" };
        public WinAdd()
        {
            InitializeComponent();

            cbType.ItemsSource = type_obj;
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearElements();
            switch (cbType.SelectedIndex)
            {
                case (0):
                {
                    ClearElements();
                    HiddenGTP();
                    break;
                }
                case (1):
                {
                        ClearElements();
                        HiddenEGO();
                        VisibleGTP();
                        cbStation.ItemsSource = MainWindow.stations;
                        break;
                }
                case (2):
                    {
                        ClearElements();
                        VisibleEGO();
                        cbStation.ItemsSource = MainWindow.stations; 
                        break;
                    }
            }
        }

        private void ClearElements()
        {
            cbStation.ItemsSource = null;
            cbGTP.ItemsSource = null;
            tbCode.Text = null;
            tbName.Text = null;
            tbPmax.Text = null;
            tbPmin.Text = null;
        }
        private void HiddenEGO()
        {
            tbPmax.Visibility = Visibility.Hidden;
            tbPmin.Visibility = Visibility.Hidden;
            lblPmax.Visibility = Visibility.Hidden;
            lblPmin.Visibility = Visibility.Hidden;
            cbGTP.Visibility = Visibility.Hidden;
            lblGTP.Visibility = Visibility.Hidden;  
        }
        private void VisibleEGO()
        {
            VisibleGTP();
            tbPmax.Visibility = Visibility.Visible;
            tbPmin.Visibility = Visibility.Visible;
            lblPmax.Visibility = Visibility.Visible;
            lblPmin.Visibility = Visibility.Visible;
            cbGTP.Visibility = Visibility.Visible;
            lblGTP.Visibility = Visibility.Visible;
        }
        private void HiddenGTP()
        {
            HiddenEGO();
            cbStation.Visibility = Visibility.Hidden;
            lblStation.Visibility = Visibility.Hidden;
        }
        private void VisibleGTP()
        {
            cbStation.Visibility = Visibility.Visible;
            lblStation.Visibility = Visibility.Visible;
        }

        private void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStation.SelectedIndex != -1)
            {
                cbGTP.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FillFromDB.AddGenObject(this);           
        }
    }
}
