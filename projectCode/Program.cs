using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace projectCode
{
    class TestCase
    {
        string first_name;
        string last_name;
        string student_ID;
        string student_mail;
        string gender;
        string department;
        string reasonToJoin;
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Student_ID { get; set; }
        public string Student_mail { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string ReasonToJoin { get; set; }
    }

    class Program
    {
        public static void Walkthrough(TestCase t)
        {
            IWebDriver chromeDriver = new ChromeDriver(@"../");
            RunTest(chromeDriver, t);
            IWebDriver firefoxdriver = new FirefoxDriver(@"../");
            RunTest(firefoxdriver, t);
        }
        public static void RunTest(IWebDriver driver, TestCase t)
        {
            // to be implemented

            driver.Url = @"file:///" + Path.GetFullPath(@"./formFile.html");
            //Fill the login credentials
            var firstNameEntry = driver.FindElement(By.Id("first_name"));
            firstNameEntry.SendKeys(t.First_name);
            var lastNameEntry = driver.FindElement(By.Id("last_name"));
            lastNameEntry.SendKeys(t.Last_name);
            var studentIDEntry = driver.FindElement(By.Id("student_ID"));
            studentIDEntry.SendKeys(t.Student_ID);
            var studentMailEntry = driver.FindElement(By.Id("student_mail"));
            studentMailEntry.SendKeys(t.Student_mail);

            if (t.Gender.Equals("male"))
                driver.FindElement(By.Id("male")).Click();
            else if (t.Gender.Equals("female"))
                driver.FindElement(By.Id("female")).Click();


            var option = driver.FindElement(By.Id("department"));
            var selectElement = new SelectElement(option);
            selectElement.SelectByText(t.Department);


            var textArea = driver.FindElement(By.Id("reasonToJoin"));
            textArea.Click();
            if(t.ReasonToJoin == null )
                textArea.SendKeys("NO REAZZZOOONNN");
            else 
                textArea.SendKeys(t.ReasonToJoin);
            driver.FindElement(By.Id("submit")).Click();
            // don't forget to close the browser after finishing the test case
            System.Threading.Thread.Sleep(2000);
            driver.Close();

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("WALKTHROUGH STARTED");
            StreamReader r = new StreamReader(Path.GetFullPath(@"../automatedTestCases.json"));
            string yourJson = r.ReadToEnd();
            // Console.WriteLine(yourJson);
            var testCases = JsonConvert.DeserializeObject<Dictionary<string, List<TestCase>>>(yourJson);
            var testCasesList = testCases["testCases"];
            foreach (var tcase in testCasesList)
                Walkthrough(tcase);
            Console.WriteLine("WALKTHROUGH FINISHED");

        }
    }
}
