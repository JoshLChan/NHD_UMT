using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            option.Content = name;
            option.Margin = new Thickness(10);
            option.Click += Click;


            parent.Children.Add(option);
            Grid.SetColumn(option, col);
            Grid.SetRow(option, row);

        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.selected_display = new Display(_name, "", "", "", "", _path);
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow) window.Close();
            }
        }
    }
}
