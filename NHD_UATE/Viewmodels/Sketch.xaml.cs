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
            //ProcessStartInfo clone_startinfo = new ProcessStartInfo();
            //clone_startinfo.FileName = "DisplaySwitch.exe";
            //clone_startinfo.Arguments = "/extend";
            //clone_startinfo.CreateNoWindow = true;

            //Process clone_display = new Process();
            //clone_display.StartInfo = clone_startinfo;
            //clone_display.Start();
        }

        private void Stop_HDMI_Click(object sender, RoutedEventArgs e)
        {
            Image_HDMI imageWin = new Image_HDMI();
            imageWin.Show();
            this.Close();

        }
    }
}
