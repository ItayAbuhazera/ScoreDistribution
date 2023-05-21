using System;
using Backend;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp1.View;


namespace WpfApp1.VIew;

public partial class CourseDetails : Page
{
    private ScoreDistributionScraper scraper;
    private CourseSyllabusScraper scraper2;
    private DispatcherTimer timer;
    private int countdownSeconds = 5 * 60;
    public CourseDetails(ScoreDistributionScraper scraper)
    {
        this.scraper = scraper;
        InitializeComponent();
        timer = new DispatcherTimer();
        timer.Interval = new System.TimeSpan(0, 0, 1);
        timer.Tick += Timer_Tick;
        timer.Start();
    }
    
    private void Timer_Tick(object sender, System.EventArgs e)
    {
        countdownSeconds--;
        if (countdownSeconds == 0)
        {
            timer.Stop();
            MessageBox.Show("You have been logged out due to inactivity");
            scraper.Quit();
            SecondaryFrame.NavigationService.Navigate(new MainWindow());
        }
        else
        {
            TimeSpan time = TimeSpan.FromSeconds(countdownSeconds);
            string str = time.ToString(@"mm\:ss");
            timerLabel.Content = str;
        }
    }
    private void ScoreButton_OnClick(object sender, RoutedEventArgs e)
    {
        //when the find button is clicked, the user will be redirected to the score distribution page
        //the user will be able to see the score distribution of the course he chose
        string courseId = this.CourseID.Text;
        string departmentNumber = this.DepartmentNum.Text;
        string courseYear = this.Year.Text;
        string courseSemester = this.Semester.Text;
        string degreeLevel = this.DegreeLevel.Text;
        scraper.GetScoreDistribution(courseId, courseYear, courseSemester, departmentNumber, degreeLevel);

    }
    private void OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        // Check if the Enter key was pressed
        if (e.Key == Key.Enter)
        {
            // If the focus is on a TextBox, move the focus to the next control in the tab order
            if (Keyboard.FocusedElement is TextBox textBox)
            {
                ScoreButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            // If the focus is on the LoginButton, simulate a click event
            else if (Keyboard.FocusedElement == ScoreButton)
            {
                ScoreButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            else if (Keyboard.FocusedElement == SyllabusButton)
            {
                SyllabusButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }

            // Mark the event as handled so that it doesn't propagate further
            e.Handled = true;
        }
    }

    private void SyllabusButton_OnClick(object sender, RoutedEventArgs e)
    {
        scraper2 = new CourseSyllabusScraper();
        string courseId = this.CourseID.Text;
        string departmentNumber = this.DepartmentNum.Text;
        string courseYear = this.Year.Text;
        string courseSemester = this.Semester.Text;
        string degreeLevel = this.DegreeLevel.Text;
        try
        {
            scraper2.GoCoursePDF(departmentNumber, degreeLevel, courseId, courseYear, courseSemester);
        }catch(Exception exp)
        {
            MessageBox.Show("The syllabus is not available" + "\n" + exp.Message);
        }
    }
}
//<Frame x:Name="MainFrame" NavigationUIVisibility="Hidden"></Frame>
