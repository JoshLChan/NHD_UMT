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
            mainwin.selected_display = new Display(_name, "", "", "", "", _path);
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow) window.Close();
            }
        }
    }
}
