using Backend;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.VIew;

public partial class CourseDetails : Page
{
    ScoreDistributionScraper scraper;
    public CourseDetails(ScoreDistributionScraper scraper)
    {
        this.scraper = scraper;
        InitializeComponent();
    }

    private void FindButton_OnClick(object sender, RoutedEventArgs e)
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
}    
//<Frame x:Name="MainFrame" NavigationUIVisibility="Hidden"></Frame>
