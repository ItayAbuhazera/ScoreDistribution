using System;
using System.Reflection;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the PATH environment variable to include the current solution folder
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Environment.GetEnvironmentVariable("PATH");
            path = $"{path};{currentDirectory}";
            Environment.SetEnvironmentVariable("PATH", path);

            // Create a new instance of the ScoreDistributionScraper class
            ScoreDistributionScraper scraper = new ScoreDistributionScraper();
            CourseSyllabusScraper scraper2 = new CourseSyllabusScraper();

            // Login to the university system

            // Get the score distribution for a specific course, year, semester, department number, and degree level
            //scraper.GetScoreDistribution(3305, 2019, 1, 372, 1);
            string courseDepartment = "202";
            string courseNumber = "2051";
            string courseYear = "2019";
            string courseSemester = "2";
            string courseDegreeLevel = "1";
            scraper2.GoCoursePDF(courseDepartment, courseDegreeLevel, courseNumber, courseYear, courseSemester);

            // Quit the web driver and free resources
            scraper.Quit();

        }
    }
}
