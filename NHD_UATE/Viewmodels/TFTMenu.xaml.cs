using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NHD_UATE.Viewmodels
{
    /// <summary>
    /// Interaction logic for DisplayMenu.xaml
    /// </summary>
    public partial class TFTMenu : Window
    {
        public TFTMenu()
        {
            InitializeComponent();
        }

        private void EVE_Click(object sender, RoutedEventArgs e)
        {
            Displays eve = new Displays("TFT/EVE");
            eve.Show();
        }

        private void HDMI_Click(object sender, RoutedEventArgs e)
        {
            Displays hdmi = new Displays("TFT/HDMI");
            hdmi.Show();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
