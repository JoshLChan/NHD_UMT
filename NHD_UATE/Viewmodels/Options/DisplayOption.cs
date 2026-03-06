using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NHD_UATE.Viewmodels.Options
{
    public class DisplayOption
    {
        public string _name;
        public string _path;

        public DisplayOption(int col, int row, Grid parent, string filePath, string name)
        {
            _path = filePath;
            _name = name;
            Button option = new Button();
            TextBlock text = new TextBlock();

            
            text.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
            text.FontSize = 20;
            text.Text = name;
            text.TextWrapping = TextWrapping.Wrap;

            option.Margin = new Thickness(10);
            option.Click += Click;
            option.Content = text;



            parent.Children.Add(option);
            Grid.SetColumn(option, col);
            Grid.SetRow(option, row);

        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var mainwin = (MainWindow)Application.Current.MainWindow;
            string[] lines = File.ReadAllLines(System.IO.Path.Combine(_path + "/" + _name + "/" + _name + "_conf.csv"));

            IEnumerable<Display> conf = lines.Select(line =>
            {
                string[] data = line.Split('\t');
                // We return a person with the data in order.
                return new Display(data[0], data[1], data[2], data[3], data[4], "");
            });

            mainwin.selected_display = new Display(_name, "", "", conf.ElementAt(1).Interface, conf.ElementAt(1).Logic, _path);
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow) window.Close();
            }
        }
    }
}
