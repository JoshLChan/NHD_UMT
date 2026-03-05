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
    public partial class DisplayMenu : Window
    {
        public DisplayMenu()
        {
            InitializeComponent();
        }

        private void TFT_Displays_Click(object sender, RoutedEventArgs e)
        {
            TFTMenu tft = new TFTMenu();
            tft.Show();
        }

        private void LCD_Displays_Click(object sender, RoutedEventArgs e)
        {
            LCDMenu lcd = new LCDMenu();
            lcd.Show();
        }

        private void OLED_Displays_Click(object sender, RoutedEventArgs e)
        {
            OLEDMenu OLED = new OLEDMenu();
            OLED.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
