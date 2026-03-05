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
        private Display _selected_display;

        public Display selected_display
        {
            get { return _selected_display; }
            set
            {
                _selected_display = value;
                
                Update_GUI();

            }
        }

        public MainWindow()
        {
            InitializeComponent();


            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {

                COMA.Items.Add(port);
                COMB.Items.Add(port);

            }

            try
            {
                if (COMA.Items.Contains("COM12"))
                {
                    COMA.Text = "COM12";
                    Reset_MCU(1);
                }
                if (COMB.Items.Contains("COM4"))
                {
                    COMB.Text = "COM4";
                    Reset_MCU(2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void display_select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Update_GUI()
        {
            Test.IsEnabled = true;
            Stop.IsEnabled = true;
            disp_image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(_selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".png"), UriKind.Absolute));
            output_image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(_selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + "_output.jpg"), UriKind.Absolute));
            //string[] lines = File.ReadAllLines(System.IO.Path.Combine(_selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + "_conf.csv"));

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
            Reset_MCU(1);
            Reset_MCU(2);

        }

        private void Upload_Firmware()
        {
            console.Clear();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd";
            if (selected_display.Logic == "5V")
            {
                Reset_MCU(2);
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -P" + COMA.Text + " -b115200 -D -Uflash:w:" + _selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".hex" + ":i";
            }
            else if (selected_display.Logic == "3.3V")
            {
                Reset_MCU(1);
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega328p -carduino -P" + COMB.Text + " -b115200 -D -Uflash:w:" + _selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".hex" + ":i";
            }

            //startInfo.Arguments = "/c ipconfig";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            //startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process HEX_upload = new Process();
            HEX_upload.StartInfo = startInfo;

            console.AppendText(_selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".hex\n\n");

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

        private void Reset_MCU(int MCU)
        {
            console.Clear();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd";

            if (MCU == 1)
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -P" + COMA.Text + " -b115200 -D -Uflash:w:N:/Testers/Production_Testers/NHD-UMT/Blanking/Blank.hex:i";
            }
            else if (MCU == 2)
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega328p -carduino -P" + COMB.Text + " -b115200 -D -Uflash:w:N:/Testers/Production_Testers/NHD-UMT/Blanking/Blank.hex:i";
            }

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process HEX_upload = new Process();
            HEX_upload.StartInfo = startInfo;

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Reset_MCU(1);
            Reset_MCU(2);
        }

        private void Select_Display_Click(object sender, RoutedEventArgs e)
        {
            DisplayMenu menu = new DisplayMenu();
            menu.Show();
        }
    }
}