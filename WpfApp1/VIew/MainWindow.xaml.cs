using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Backend;
using WpfApp1.VIew;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            string userName = this.name.Text;
            string password = this.Password.Password;
            string id = this.ID.Text;
            ScoreDistributionScraper scraper = new ScoreDistributionScraper();
            try
            {
                scraper.Login(userName, password, id);
                CourseDetails courseDetails = new CourseDetails(scraper);
                MainFrame.NavigationService.Navigate(courseDetails);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                scraper.Quit();
            }
            
        }
    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        // Check if the Enter key was pressed and if yes just click the button
        if (e.Key == Key.Enter)
        {
            // If the focus is on a TextBox, move the focus to the next control in the tab order
            if (Keyboard.FocusedElement is TextBox textBox)
            {
                LoginButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            // If the focus is on the LoginButton, simulate a click event
            else if (Keyboard.FocusedElement.Equals(LoginButton) )
            {
                LoginButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            // Mark the event as handled so that it doesn't propagate further
            e.Handled = true;
        }
    }
    }
}