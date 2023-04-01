using System;
using System.Collections.Generic;
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
                MessageBox.Show("Login");
                CourseDetails courseDetails = new CourseDetails(scraper);
                MainFrame.NavigationService.Navigate(courseDetails);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                scraper.Quit();
            }
            
        }
    }
}