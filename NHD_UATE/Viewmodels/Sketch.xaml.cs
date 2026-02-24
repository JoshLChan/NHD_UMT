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
    /// Interaction logic for Sketch.xaml
    /// </summary>
    public partial class Sketch : Window
    {
        public Sketch()
        {
            InitializeComponent();
        }

        private void Stop_HDMI_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
