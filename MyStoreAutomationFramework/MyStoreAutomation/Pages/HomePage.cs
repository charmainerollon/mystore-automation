using MyStoreAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStoreAutomation
{
    public class HomePage
    {      

        public static void WaitPageTToLoad(int timeout, string pageTitle)
        {
            BrowsersFactory.GetDriver.Wait(timeout).ForPage().ReadyStateComplete();
            BrowsersFactory.GetDriver.Wait(timeout).ForPage().TitleToEqual(pageTitle);
        }

        public static void SearchOrder(string search_value)
        {
            BrowsersFactory.GetDriver.FindElement(By.Id("search_query_top")).SendKeys(search_value);
            Thread.Sleep(3000);
            BrowsersFactory.GetDriver.FindElement(By.Name("submit_search")).Click();
            HomePage.WaitPageTToLoad(10000, "Search - My Store");
        }    
    }
}


