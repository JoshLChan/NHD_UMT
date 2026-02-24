using NHD_UATE.Viewmodels;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NHD_UATE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        string main_dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        string displayFolder;
        Display selected_display;

        public MainWindow()
        {
            InitializeComponent();

            // The folder for the roaming current user 


            // Combine the base folder with your specific folder....
            displayFolder = System.IO.Path.Combine(main_dir, "NHD-UATE/Displays");

            Debug.WriteLine(displayFolder);

            Directory.CreateDirectory(displayFolder);

            string[] entries = Directory.GetDirectories(displayFolder, "*", SearchOption.AllDirectories).Select(d => new DirectoryInfo(d).Name).ToArray();

            foreach (var entry in entries)
            {
                display_select.Items.Add(entry);
            }

            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                COMA.Items.Add(port);
                COMB.Items.Add(port);
            }

        }

        private void display_select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test.IsEnabled = true;
            Stop.IsEnabled = true;
            disp_image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + ".png"), UriKind.Absolute));
            output_image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + "_output.jpg"), UriKind.Absolute));
            string[] lines = File.ReadAllLines(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + "_conf.csv"));

            IEnumerable<Display> conf = lines.Select(line =>
            {
                string[] data = line.Split('\t');
                // We return a person with the data in order.
                return selected_display = new Display(data[0], data[1], data[2], data[3], data[4]);
            });

            selected_display = new Display(conf.ElementAt(1).Name, conf.ElementAt(1).Description, conf.ElementAt(1).Rev, conf.ElementAt(1).Interface, conf.ElementAt(1).Logic);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            

            if (selected_display.Interface == "HDMI")
            {
                Test_HDMI();
            }
            else
            {
                if (COMA.Text == "" || COMB.Text == "")
                {
                    MessageBox.Show("COM ports not selected", "Serial Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Upload_Firmware();
                }
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            console.Clear();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd";
            startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -PCOM12 -b115200 -D -Uflash:w:" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Blanking/Blank.hex:i";
            //startInfo.Arguments = "/c ipconfig";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process HEX_upload = new Process();
            HEX_upload.StartInfo = startInfo;
            console.AppendText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Blanking/Blank.hex\n\n");

            HEX_upload.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler((sender, e) =>
            {
                //Debug.WriteLine("e.Data);
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    console.AppendText(e.Data + '\n');
                    console.ScrollToEnd();
                }));

            });
            HEX_upload.Start();
            HEX_upload.BeginErrorReadLine();

        }

        private void Upload_Firmware()
        {
            console.Clear();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd";
            if (selected_display.Logic == "5V")
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -P" + COMA.Text + " -b115200 -D -Uflash:w:" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + ".hex" + ":i";
            }
            else if (selected_display.Logic == "3.3V")
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega328p -carduino -P" + COMB.Text + " -b115200 -D -Uflash:w:" + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + ".hex" + ":i";
            }

            //startInfo.Arguments = "/c ipconfig";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process HEX_upload = new Process();
            HEX_upload.StartInfo = startInfo;

            console.AppendText(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NHD-UATE/Displays/" + display_select.SelectedItem.ToString() + "/" + display_select.SelectedItem.ToString() + ".hex\n\n");

            HEX_upload.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler((sender, e) =>
            {
                //Debug.WriteLine("e.Data);
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    console.AppendText(e.Data + '\n');
                    console.ScrollToEnd();
                }));

            });
            HEX_upload.Start();
            HEX_upload.BeginErrorReadLine();
        }

        private void Test_HDMI()
        {
            Sketch HDMI_test = new Sketch();
            HDMI_test.Show();
        }
    }
}