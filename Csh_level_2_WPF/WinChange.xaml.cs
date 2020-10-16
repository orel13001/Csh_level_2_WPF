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
    /// Логика взаимодействия для ChangeObject.xaml
    /// </summary>
    public partial class WinChange : Window
    {
        private List<string> type_obj = new List<string>() { "Электростанция", "ГТП", "ЕГО" };
        public WinChange()
        {
            InitializeComponent();

            
        }

        private void rbDelete_Checked(object sender, RoutedEventArgs e)
        {
            ChangeRadioButton();
        }

        private void rbChange_Checked(object sender, RoutedEventArgs e)
        {
            ChangeRadioButton();
        }

        private void ChangeRadioButton()
        {
            if ((bool)rbChange.IsChecked)
            {
                cbType.ItemsSource = null;
                lblType.Visibility = Visibility.Hidden;
                cbType.Visibility = Visibility.Hidden;

                lblEGO.Visibility = Visibility.Visible;
                cbEGO.Visibility = Visibility.Visible;
                lblGTP.Visibility = Visibility.Visible;
                cbGTP.Visibility = Visibility.Visible;
                lblStation.Visibility = Visibility.Visible;
                cbStation.Visibility = Visibility.Visible;
                cbGTPnew.Visibility = Visibility.Visible;
                lblGTPnew.Visibility = Visibility.Visible;
                cbStation.ItemsSource = MainWindow.stations;
            }
            else
            {
                lblType.Visibility = Visibility.Visible;
                cbType.Visibility = Visibility.Visible;

                lblEGO.Visibility = Visibility.Hidden;
                cbEGO.Visibility = Visibility.Hidden;
                lblGTP.Visibility = Visibility.Hidden;
                cbGTP.Visibility = Visibility.Hidden;
                lblStation.Visibility = Visibility.Hidden;
                cbStation.Visibility = Visibility.Hidden;
                cbGTPnew.Visibility = Visibility.Hidden;
                lblGTPnew.Visibility = Visibility.Hidden;
                cbType.ItemsSource = type_obj;
                cbGTPnew.ItemsSource = null;
            }
        }





        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearElements();
            switch (cbType.SelectedIndex)
            {
                case (0):
                    {
                        ClearElements();
                        lblStation.Visibility = Visibility.Visible;
                        cbStation.Visibility = Visibility.Visible;
                        lblEGO.Visibility = Visibility.Hidden;
                        cbEGO.Visibility = Visibility.Hidden;
                        lblGTP.Visibility = Visibility.Hidden;
                        cbGTP.Visibility = Visibility.Hidden;
                        
                        cbStation.ItemsSource = MainWindow.stations;
                        break;
                    }
                case (1):
                    {
                        ClearElements();
                        lblStation.Visibility = Visibility.Visible;
                        cbStation.Visibility = Visibility.Visible;
                        lblEGO.Visibility = Visibility.Hidden;
                        cbEGO.Visibility = Visibility.Hidden;
                        lblGTP.Visibility = Visibility.Visible;
                        cbGTP.Visibility = Visibility.Visible;
                        
                        cbStation.ItemsSource = MainWindow.stations;
                        if (cbStation.SelectedIndex != -1)
                        {
                            cbGTP.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps;
                        }
                        break;
                    }
                case (2):
                    {
                        ClearElements();
                        lblStation.Visibility = Visibility.Visible;
                        cbStation.Visibility = Visibility.Visible;
                        lblEGO.Visibility = Visibility.Visible;
                        cbEGO.Visibility = Visibility.Visible;
                        lblGTP.Visibility = Visibility.Visible;
                        cbGTP.Visibility = Visibility.Visible;

                        cbStation.ItemsSource = MainWindow.stations;
                        if (cbStation.SelectedIndex != -1)
                        {
                            cbGTP.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps;
                        }
                        if (cbGTP.SelectedIndex != -1)
                        {
                            cbEGO.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps[cbGTP.SelectedIndex].egos;
                        }

                        break;
                    }
            }
        }

        private void ClearElements()
        {
            cbStation.ItemsSource = null;
            cbGTP.ItemsSource = null;
        }
        private void HiddenEGO()
        {
            cbGTP.Visibility = Visibility.Hidden;
            lblGTP.Visibility = Visibility.Hidden;
        }
        private void VisibleEGO()
        {
            VisibleGTP();
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
            if ((bool)rbChange.IsChecked)
            {
                cbGTPnew.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps;
            }
            cbEGO.ItemsSource = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbGTP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGTP.SelectedIndex != -1)
            {
                cbEGO.ItemsSource = MainWindow.stations[cbStation.SelectedIndex].gtps[cbGTP.SelectedIndex].egos;
            }
        }

        private void btnExec_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbChange.IsChecked)
            {
                ChangeObject.MoveEGO(this);
            }
            else
            {
                switch (cbType.SelectedIndex)
                {
                    case (0):
                        {
                            ChangeObject.DelStation(this);
                            break;
                        }
                    case (1):
                        {
                            ChangeObject.DelGTP(this);
                            break;
                        }
                    case (2):
                        {
                            ChangeObject.DelEGO(this);
                            break;
                        }
                }
            }
        }
    }
}
