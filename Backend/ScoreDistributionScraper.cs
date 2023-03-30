using System;
using System.Net;
using System.IO;
using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Backend
{ 

class ScoreDistributionScraper
{
        private IWebDriver driver;
        private string loginToken { get; set; } // This is the token that is used to get the score distribution

        public ScoreDistributionScraper()
        {
            // Initialize the web driver using the ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Run the browser in headless mode
            driver = new ChromeDriver(options);
            loginToken = "";
        }
        
        public void Login(string username, string password, string id)
        {
            // Use the web driver to login to the university system https://bgu4u22.bgu.ac.il/apex/f?p=104:101::::::
            // Return the session key
            driver.Navigate().GoToUrl("https://bgu4u22.bgu.ac.il/apex/f?p=104:101::::::");
            driver.FindElement(By.Id("P101_X1")).SendKeys(username);
            driver.FindElement(By.Id("P101_X2")).SendKeys(password);
            driver.FindElement(By.Id("P101_X3")).SendKeys(id);
            driver.FindElement(By.CssSelector("button.uButton.uHotButton")).Click();
            // Get the session key 
            var loginTokenElement = driver.FindElement(By.Id("logintoken"));
            loginToken = loginTokenElement.GetAttribute("value");
        }

            public void GetScoreDistribution(int courseId, int year, int semester,int departmentNumber,int degreeLevel)
        {
            //build the url in this form:
            //https://reports4u22.bgu.ac.il/GeneratePDF.php?server=aristo4stu419c/report=SCRR016w/p_key={loginToken}/p_year={year}/p_semester={semester}/out_institution=0/grade=5/list_department=*{departmentNumber}@/list_degree_level=*{degreeLevel}@/list_course=*{courseNumber}@/LIST_GROUP=*@/P_FOR_STUDENT=1
            //and open this url using the web driver
            //save the pdf file in the folder "ScoreDistribution"
            //the file name should be in this form: {courseId}_{year}_{semester}.pdf
            //for example: 123456_2019_1.pdf
            //if the file already exists, do not download it again

            //build the url
            string url = "https://reports4u22.bgu.ac.il/GeneratePDF.php?server=aristo4stu419c/report=SCRR016w/p_key=" + loginToken + "/p_year=" + year + "/p_semester=" + semester + "/out_institution=0/grade=5/list_department=*" + departmentNumber + "@/list_degree_level=*" + degreeLevel + "@/list_course=*" + courseId + "@/LIST_GROUP=*@/P_FOR_STUDENT=1";
            //open the url
            driver.Navigate().GoToUrl(url);
            //save the pdf file in the folder "ScoreDistribution"
            //the file name should be in this form: {courseId}_{year}_{semester}.pdf
            // Download the PDF using the WebDriver
            using (var client = new WebClient())
            {
                client.DownloadFile(url, "ScoreDistribution/" + courseId + "_" + year + "_" + semester + ".pdf");
            }




        }

        public void Quit()
        {
            // Quit the web driver and free resources
            driver.Quit();
        }
    }
}
