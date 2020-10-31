using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Text.RegularExpressions;
using System.Media;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// Note: Tried DispatchTImer but this is not accurate enough. Because this runs on the dispatcher thread whenever the thread is busy it is unable to update in time. 
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private TimeSpan time; //Amount of time to countdown 
        private System.Timers.Timer myTimer;

        private enum MAXVALUE
        {
            Hours = 12,
            Minutes = 60,
            Seconds = 60,
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //Disable editing of the boxes
            Hours.IsEnabled = false;
            Minutes.IsEnabled = false;
            Seconds.IsEnabled = false;

            //deattach event handler
            Hours.TextChanged -= confineToLimit;
            Minutes.TextChanged -= confineToLimit;
            Seconds.TextChanged -= confineToLimit;

            //Hide the start button & show the stop button
            Start.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Visible;

            int hours = ConvertToInt(Hours); //Get users input
            int minutes = ConvertToInt(Minutes); //Get users input
            int seconds = ConvertToInt(Seconds); //Get users input

            time = new TimeSpan(hours, minutes, seconds); //Set contdown timer
            Debug.WriteLine("User Input " + time.ToString()); //Debug to see what user has input

            CountDown(); //Start countdown
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            stopTheClock();
        }

        private void stopTheClock()
        {
            //Enable editing of the boxes
            Hours.IsEnabled = true;
            Minutes.IsEnabled = true;
            Seconds.IsEnabled = true;

            //re-attach event handler
            Hours.TextChanged += confineToLimit;
            Minutes.TextChanged += confineToLimit;
            Seconds.TextChanged += confineToLimit;

            Start.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Hidden;

            myTimer.Stop();
            myTimer.Close();

            SystemSounds.Beep.Play(); //Play system beep
        }

        private void CountDown()
        {
            myTimer = new System.Timers.Timer(); //Create new timer
            myTimer.Elapsed += new ElapsedEventHandler(updateTimer); //Tell the timer to call our updateTimer function every interval
            myTimer.Interval = 1000; //Set timer to tick every second
            myTimer.Enabled = true; //Start the timer
        }

        private void updateTimer(object sender, EventArgs e)
        {
            if (time.TotalSeconds <= 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    stopTheClock();
                });
            }
            else
            {
                time = time.Subtract(TimeSpan.FromSeconds(1));
                Debug.WriteLine(time.ToString()); //Debug time left

                this.Dispatcher.Invoke(() => //Updating UI outside of main thread so need to use Dispatcher here. 
                {
                    Hours.Text = time.Hours.ToString();
                    Minutes.Text = time.Minutes.ToString();
                    Seconds.Text = time.Seconds.ToString();
                });
            }
        }

        private void confineToLimit(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            String TextBoxName = txt.Name;
            int limit;

            switch (TextBoxName)
            {
                case "Hours":
                    limit = (int)MAXVALUE.Hours;
                    break;
                case "Minutes":
                    limit = (int)MAXVALUE.Minutes;
                    break;
                case "Seconds":
                    limit = (int)MAXVALUE.Seconds;
                    break;
                default:
                    throw new ArgumentException("Invalid textbox.");
            }

            int convertedInt = ConvertToInt(txt);
            if (convertedInt > limit)
            {
                txt.Text = limit.ToString();
            }
        }

        private int ConvertToInt(TextBox txt)
        {
            int convertedInt;

            try
            {
                Int32.TryParse(txt.Text, out convertedInt);
                return convertedInt;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Failure to convert String to Int");
                return 0;
            }
        }

        //Validation Methods
        private void ValidateTextBox(object sender, TextCompositionEventArgs e)
        {
            //Allow only numbers to be entered within the textbox
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //Prevent copy, cutting & pasting within the textbox
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll())); //Select all text within textbox
        }
    }
}
