using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Image.xaml
    /// </summary>
    public partial class Image_HDMI : Window
    {
        public Image_HDMI()
        {
            InitializeComponent();
        }

        private void Stop_HDMI_Click(object sender, RoutedEventArgs e)
        {
            Image_HDMI imageWin = new Image_HDMI();
            imageWin.Show();
            this.Close();

        }
    }
}
