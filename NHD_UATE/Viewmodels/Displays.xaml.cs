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

            string[] entries = Directory.GetDirectories(displayFolder, "*", SearchOption.TopDirectoryOnly).Select(d => new DirectoryInfo(d).Name).ToArray();

            int x = 0;
            int y = 0;
            int page = 0;

            foreach (var entry in entries)
            {
                

                Debug.WriteLine(x + " | " + y);
                DisplayOption option = new DisplayOption(x, y, main_grid, displayFolder, entry);


                
                if (x < page + 3) { x++; }
                if (y < 4 && x == page + 3) { y++; x = page; }
                if (y == 4 && x == page + 3) { y = 0; page += 3; x = page; }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
