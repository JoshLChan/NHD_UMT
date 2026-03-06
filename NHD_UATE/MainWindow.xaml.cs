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

        string COMA_Index = "COM4";
        string COMB_Index = "COM3";

        bool hasCOMA = false;
        bool hasCOMB = false;

        public MainWindow()
        {
            InitializeComponent();
            Init_MCU(true);

        }

        private void Init_MCU(bool reset)
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            hasCOMA = false;
            hasCOMB = false;

            foreach (string port in ports)
            {

                try
                {
                    if (port == COMA_Index)
                    {
                        if (reset) Reset_MCU(1);
                        hasCOMA = true;
                    }

                    if (port == COMB_Index)
                    {
                        if (reset) Reset_MCU(2);
                        hasCOMB = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }

            if (hasCOMA && hasCOMB)
            {
                Select_Display.IsEnabled = true;
                MCU_Status.Text = "CONNECTED";
                MCU_Status.Foreground = Brushes.Green;

                Test.IsEnabled = true;
                Stop.IsEnabled = true;
            }
            else
            {
                Test.IsEnabled = false;
                Stop.IsEnabled = false;
                MessageBox.Show("MCU Interface Not Connected. Please Turn On and Reconnect MCU Interface ", "WARNING", MessageBoxButton.OK, MessageBoxImage.Error);
                Select_Display.IsEnabled = false;
                MCU_Status.Text = "NOT CONNECTED";
                MCU_Status.Foreground = Brushes.Red;
            }
        }

        private void display_select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Update_GUI()
        {
            Init_MCU(false);

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
                if (COMA_Index == "" || COMB_Index == "")
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

            Init_MCU(false);

            if (hasCOMA && hasCOMB)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd";
                if (selected_display.Logic == "5V")
                {
                    Reset_MCU(2);
                    startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -P" + COMA_Index + " -b115200 -D -Uflash:w:" + _selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".hex" + ":i";
                }
                else if (selected_display.Logic == "3.3V")
                {
                    Reset_MCU(1);
                    startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega328p -carduino -P" + COMB_Index + " -b115200 -D -Uflash:w:" + _selected_display.Path + "/" + _selected_display.Name + "/" + _selected_display.Name + ".hex" + ":i";
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

        }

        private void Reset_MCU(int MCU)
        {
            console.Clear();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd";

            if (MCU == 1)
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega2560 -cwiring -P" + COMA_Index + " -b115200 -D -Uflash:w:N:/Testers/Production_Testers/NHD-UMT/Blanking/Blank.hex:i";
            }
            else if (MCU == 2)
            {
                startInfo.Arguments = "/c cd AvrDude/ && avrdude -v -V -patmega328p -carduino -P" + COMB_Index + " -b115200 -D -Uflash:w:N:/Testers/Production_Testers/NHD-UMT/Blanking/Blank.hex:i";
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

        private void Reconnect_MCU_Click(object sender, RoutedEventArgs e)
        {
            Init_MCU(true);
        }
    }
}