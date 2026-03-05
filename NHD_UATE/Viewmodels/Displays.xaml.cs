using NHD_UATE.Viewmodels.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for Displays.xaml
    /// </summary>
    public partial class Displays : Window
    {
        string main_dir = "N:/Testers/Production_Testers/NHD-UMT/";
        public Displays(string rel_path)
        {
            InitializeComponent();

            // Combine the base folder with your specific folder....
            string displayFolder = System.IO.Path.Combine(main_dir, "Displays/" + rel_path);

            Debug.WriteLine(displayFolder);

            Directory.CreateDirectory(displayFolder);

            string[] entries = Directory.GetDirectories(displayFolder, "*", SearchOption.AllDirectories).Select(d => new DirectoryInfo(d).Name).ToArray();

            int x = 0;
            int y = 0;

            foreach (var entry in entries)
            {
                Debug.WriteLine(entry);
                DisplayOption option = new DisplayOption(x, y, main_grid, displayFolder, entry);


                
                if (x < 11) { x++; } else { x = 0; }
                if (y < 4 && x == 0) { y++; } else if (y >= 4) { y = 0; }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
