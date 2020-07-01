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

        enum MAXVALUE
        {
            Hours = 24,
            Minutes = 60,
            Seconds = 60,
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hours = Hours.Text; //Get users input
            string minutes = Minutes.Text; //Get users input
            string seconds = Seconds.Text; //Get users input

            System.Diagnostics.Debug.WriteLine(hours + "" + minutes + "" + seconds); //Debug to see what user has input
            //InterpretInput(userInput);


            //DispatcherTimer dispatcherTimer = new DispatcherTimer();
        }

        private void InterpretInput(string userInput)
        {

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
