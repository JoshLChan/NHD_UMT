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
    public partial class LCDMenu : Window
    {
        public LCDMenu()
        {
            InitializeComponent();
        }

        private void Graphic_LCD_Click(object sender, RoutedEventArgs e)
        {
            Displays graphic = new Displays("LCD/Graphic");
            graphic.Show();
        }

        private void Character_LCD_Click(object sender, RoutedEventArgs e)
        {
            Displays character = new Displays("LCD/Character");
            character.Show();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
