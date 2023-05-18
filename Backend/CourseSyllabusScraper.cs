using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.WebRequestMethods;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Backend
{
    public class CourseSyllabusScraper
    {
        private IWebDriver driver;

        public CourseSyllabusScraper()
        {
            // Initialize the web driver using the ChromeDriver
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless"); // Run the browser in headless mode
            //options.AddArguments("--auto-open-devtools-for-tabs");
            driver = new ChromeDriver(options);
        }
        
        public void GoCoursePDF(string courseDepartment, string courseDegreeLevel, string course,
            string courseYear, string courseSemester)
        {
            if (courseDepartment == "" || courseDegreeLevel == "" || course == "" || courseYear == "" || courseSemester == "")
            {
                Console.WriteLine("Please fill all the details");
                throw new Exception("Please fill all the details");
            }
            driver.Navigate().GoToUrl("https://bgu4u.bgu.ac.il/pls/scwp/!app.gate?app=ann");
            var frameElement = driver.FindElement(By.Name("main"));
            // Switch to the frame
            driver.SwitchTo().Frame(frameElement);
            // Click on the "חיפוש מורחב" link to go to the advanced search page
            IWebElement searchLink = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                searchLink = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/center/d1iv/table[2]/tbody/tr/td/table/tbody/tr[2]/td[2]/div/a[2]")));
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element not found within the specified time.");
                return; // Or handle the error as per your requirements
            }
            searchLink.Click();

            // Find and fill in the input fields with the required information
            IWebElement courseDepartmentInput = driver.FindElement(By.Id("on_course_department"));
            courseDepartmentInput.SendKeys(courseDepartment);

            IWebElement courseDegreeLevelInput = driver.FindElement(By.Id("on_course_degree_level"));
            courseDegreeLevelInput.SendKeys(courseDegreeLevel);

            IWebElement courseInput = driver.FindElement(By.Id("on_course"));
            courseInput.SendKeys(course);

            IWebElement yearInput = driver.FindElement(By.Id("on_year"));
            yearInput.SendKeys(courseYear);

            IWebElement semesterInput = driver.FindElement(By.Id("on_semester"));
            semesterInput.SendKeys(courseSemester);

            // Submit the form to search for the course
            IWebElement submitButton = driver.FindElement(By.CssSelector("input[type='button']"));
            submitButton.Click();
            // Find the element using XPath
            var element = driver.FindElement(By.XPath("//a[contains(@href, 'goCourseSemester')]"));

            // Click on the element
            element.Click();
            // Find the link to the syllabus and navigate to it
            driver.FindElement(By.XPath("//a[contains(@href, 'goCoursePDF')]")).Click();


            // Initialize a new instance of ChromeDriver without headless mode
            //var newDriver = new ChromeDriver();
            //var headlessWindowHandle = driver.CurrentWindowHandle;
            //newDriver.SwitchTo().Window(headlessWindowHandle);
            // Switch to the newly opened browser window

            // Continue the necessary steps to reach the last page
            // Find and click the last button to open Chrome browser
            //newDriver.FindElement(By.XPath("//a[contains(@href, 'goCourseSemester')]")).Click();



        }
    }
}
