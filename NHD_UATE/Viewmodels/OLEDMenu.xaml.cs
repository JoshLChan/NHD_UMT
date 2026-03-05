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
    public partial class OLEDMenu : Window
    {
        public OLEDMenu()
        {
            InitializeComponent();
        }

        private void Graphic_OLED_Click(object sender, RoutedEventArgs e)
        {
            Displays graphic = new Displays("OLED/Graphic");
            graphic.Show();
        }

        private void Character_OLED_Click(object sender, RoutedEventArgs e)
        {
            Displays character = new Displays("OLED/Character");
            character.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
