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
            
            ProcessStartInfo extend_startinfo = new ProcessStartInfo();
            extend_startinfo.FileName = "DisplaySwitch.exe";
            extend_startinfo.Arguments = "/extend";
            extend_startinfo.CreateNoWindow = true;

            Process extend_display = new Process();
            extend_display.StartInfo = extend_startinfo;
            extend_display.Start();

            this.Close();
        }
    }
}
