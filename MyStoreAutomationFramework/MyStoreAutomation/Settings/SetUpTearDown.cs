using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyStoreAutomation
{
    public class SetupTearDown
    {
        [OneTimeSetUp]
        public void BeforeTest()
        {
            BrowsersFactory.LoadApplication();           
        }

        [OneTimeTearDown]
        public void AfterTest()
        {
            BrowsersFactory.GetDriver.Close();
            BrowsersFactory.GetDriver.Quit();
        }

    }
}