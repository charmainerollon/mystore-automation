using AngleSharp.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreAutomation
{
    class BrowsersFactory
    {
        private static IWebDriver driver;

        public static JObject configuration = JObject.Parse(File.ReadAllText("Settings\\Configuration.json"));
        public static void LoadApplication()
        {
            //IWebDriver driver;
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("window-size=1920,1080");
            if ((string)configuration["Headless"] == "True") options.AddArguments("headless");
            options.AddArguments("hide-scrollbars");

            options.AddArguments(
                "incognito",
                "disable-gpu",
                "disable-extensions",
                "proxy-server='direct://'",
                "proxy-bypass-list=*",
                "no-sandbox",
                "disable-dev-shm-usage",
                "--allow-running-insecure-content"
                    );

            driver = new ChromeDriver(options);

           
        }

        public static void GoToMainPage()
        {
            string getUrl = Convert.ToString((string)configuration["Url"]);

            driver.Navigate().GoToUrl(getUrl);

            // Maximize page
            driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(10000);

            driver.Manage().Window.Maximize();
        }
        public static IWebDriver GetDriver
        {
            get { return driver; }
        }

        public static WebDriverWait WaitForElement(string elementName)
        {

            WebDriverWait wait = new WebDriverWait(
                GetDriver,
                TimeSpan.FromMilliseconds(10000)
                );
            wait.PollingInterval = TimeSpan.FromMilliseconds(3000);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            wait.Message = "Waited element " + elementName + " not found";

            return wait;
        }

    }
}
