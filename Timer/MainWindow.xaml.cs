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

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        TimeSpan time; //Amount of time to countdown 

        enum MAXVALUE
        {
            Hours = 24,
            Minutes = 60,
            Seconds = 60,
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Hours.IsEnabled = false;
            Minutes.IsEnabled = false;
            Seconds.IsEnabled = false; 

            int hours = ConvertToInt(Hours); //Get users input
            int minutes = ConvertToInt(Minutes); //Get users input
            int seconds = ConvertToInt(Seconds); //Get users input

            time = new TimeSpan(hours,minutes,seconds); //Set contdown timer
            System.Diagnostics.Debug.WriteLine("User Input " + time.ToString()); //Debug to see what user has input

            CountDown(); //Start countdown
        }

        private void CountDown()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //Set how often tick triggers (Every 1 second)
            timer.Tick += new EventHandler(updateTimer); //Call updateTimer function every tick 
            timer.Start(); //Start 
        }

        private void updateTimer(object sender, EventArgs e)
        {
            time = time.Subtract(TimeSpan.FromSeconds(1));

            System.Diagnostics.Debug.WriteLine(time.ToString()); //Debug to see what user has input

            Hours.Text = time.Hours.ToString();
            Minutes.Text = time.Minutes.ToString();
            Seconds.Text = time.Seconds.ToString();
        }







        //EVENT FUNCTIONS
        private void Hours_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int limit = (int)MAXVALUE.Hours;

            int convertedInt = ConvertToInt(txt);

            if (convertedInt > limit)
            {
                txt.Text = limit.ToString();
            }
        }

        private void Minutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
        }

        private void Seconds_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
        }

        //HELPER FUNCTIONS
        private int ConvertToInt(TextBox txt)
        {
            int convertedInt;

            try
            {
                Int32.TryParse(txt.Text, out convertedInt);
                return convertedInt;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Failure to convert String to Int");
                return 0;
            }
        }

        //OLD CODE FOR USING ONE INPUT BOX FOR HOURS MINUTES & SECONDS

        /*private void InterpretInput(string userInput)
        {
            userInput = userInput.Trim(); //Trim whitespace on input
            string[] seperators = { " ", ",", "." }; //List of strings we want to seperate text with
            List<string> split = userInput.Split(seperators, StringSplitOptions.RemoveEmptyEntries).ToList(); //Split the users input using our seperators

            foreach (string item in split)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }*/

    }
}
