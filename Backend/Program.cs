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

            // Login to the university system
            scraper.Login("itayab", "ItAb2023", "207217654");

            // Get the score distribution for a specific course, year, semester, department number, and degree level
            //scraper.GetScoreDistribution(3305, 2019, 1, 372, 1);

            // Quit the web driver and free resources
            scraper.Quit();

            Console.WriteLine("Score distribution downloaded successfully!");
        }
    }
}
