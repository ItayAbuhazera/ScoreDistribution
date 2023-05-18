using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.WebRequestMethods;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Net.Http;
using System.Web.Services.Description;

namespace Backend
{ 

public class ScoreDistributionScraper
{
        private IWebDriver _driver;
        private string LoginToken { get; set; } // This is the token that is used to get the score distribution

        public ScoreDistributionScraper()
        {
            // Initialize the web driver using the ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Run the browser in headless mode
            //options.AddArguments("--auto-open-devtools-for-tabs");
            _driver = new ChromeDriver(options);
            LoginToken = "";
        }
        
        public void Login(string username, string password, string id)
        {
            
            //check if the user put all the details
            if (username == "" || password == "" || id == "")
            {
                Console.WriteLine("Please fill all the details");
                throw new Exception("Please fill all the details");
            }
            
            // Use the web driver to login to the university system https://bgu4u22.bgu.ac.il/apex/f?p=104:101::::::
            // Return the session key
            _driver.Navigate().GoToUrl("https://bgu4u22.bgu.ac.il/apex/f?p=104:101::::::");
            _driver.FindElement(By.Id("P101_X1")).SendKeys(username);
            _driver.FindElement(By.Id("P101_X2")).SendKeys(password);
            _driver.FindElement(By.Id("P101_X3")).SendKeys(id);
            _driver.FindElement(By.CssSelector("button.uButton.uHotButton")).Click();
            if (_driver.Url != "https://bgu4u22.bgu.ac.il/apex/wwv_flow.accept")
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Login failed!");
                throw new Exception("Login failed!");
            }
            //open the specific page that has the score distribution:  https://bgu4u22.bgu.ac.il/apex/f?p=109:3
            //get the current url
            String currentUrl = _driver.Url;
            //get the login key with substring
            Console.WriteLine(currentUrl);
            String[] parts = currentUrl.Split(':');
            String loginKey = parts[3];
            Console.WriteLine(loginKey);
            //get the token with opening some ditribution
            _driver.Navigate().GoToUrl("https://bgu4u22.bgu.ac.il/apex/f?p=109:3:" + loginKey);
            _driver.FindElement(By.XPath("/html/body/form/div/div/div/div/div/section/div[2]/button")).Click();
            //switch to the newly opened window
            String newWindowHandle = _driver.WindowHandles.Last();
            _driver.SwitchTo().Window(newWindowHandle);
            //get the new url as string
            String newWindowUrl = _driver.Url;            
            Console.WriteLine(newWindowUrl);
            // Get the session key 
            string finalUrl = _driver.Url;
            int startIndex = finalUrl.IndexOf("p_key=") + 6; // find the start index of p_key value
            int endIndex = finalUrl.IndexOf('/', startIndex); // find the end index of p_key value
            LoginToken = finalUrl.Substring(startIndex, endIndex - startIndex); // extract p_key value
            Console.WriteLine(LoginToken);
        }

            public void GetScoreDistribution(string courseId, string year, string semester,string departmentNumber,string degreeLevel)
        {
            //build the url in this form:
            //https://reports4u22.bgu.ac.il/GeneratePDF.php?server=aristo4stu419c/report=SCRR016w/p_key={loginToken}/p_year={year}/p_semester={semester}/out_institution=0/grade=5/list_department=*{departmentNumber}@/list_degree_level=*{degreeLevel}@/list_course=*{courseNumber}@/LIST_GROUP=*@/P_FOR_STUDENT=1
            //and open this url using the web driver
            //save the pdf file in the folder "ScoreDistribution"
            //the file name should be in this form: {courseId}_{year}_{semester}.pdf
            //for example: 123456_2019_1.pdf
            //if the file already exists, do not download it again
            //check if the directory exist and if not create one
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            string directoryPath = "ScoreDistribution";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            //build the url
            string url = "https://reports4u22.bgu.ac.il/GeneratePDF.php?server=aristo4stu419c/report=SCRR016w/p_key=" + LoginToken + "/p_year=" + year + "/p_semester=" + semester + "/out_institution=0/grade=5/list_department=*" + departmentNumber + "@/list_degree_level=*" + degreeLevel + "@/list_course=*" + courseId + "@/LIST_GROUP=*@/P_FOR_STUDENT=1";
            //open the url
            _driver.Navigate().GoToUrl(url);
            //switch to the newly opened window
            // Wait for the new URL to appear
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.PollingInterval = TimeSpan.FromMilliseconds(3000);


        }
    public void Quit()
        {
            // Quit the web driver and free resources
            //driver.Quit();
        }
    }
}
