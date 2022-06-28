using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStoreAutomation.Pages
{
    public class OrderPage
    {
        public static void AddToCart()
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(text(), 'Add to cart')]")).Click();

            BrowsersFactory.WaitForElement("IconOk")
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class, 'icon-ok')]")));

            BrowsersFactory.WaitForElement("ProccedToCheckout")
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class, 'button-container')]//*[contains(text(), 'Proceed to checkout')]")));
        }

        public static void ProceedToCheckout()
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'button-container')]//*[contains(text(), 'Proceed to checkout')]")).Click();

            HomePage.WaitPageTToLoad(10000, "Order - My Store");
        }
        public static void SelectOrder(string price_value, string color_value, string size_value, string quantity_value)
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'product-price')][contains(text(), '" + price_value + "')]//ancestor::*[contains(@class, 'right-block')]//*[contains(@class, 'available-now')]")).Click();
            Thread.Sleep(2000);
            BrowsersFactory.GetDriver.FindElement(By.XPath(" //*[contains(@class, 'product-price')][contains(text(), '" + price_value + "')]//ancestor::*[contains(@class, 'right-block')]//*[text() ='More']")).Click();
            HomePage.WaitPageTToLoad(10000, "Printed Summer Dress - My Store");

            BrowsersFactory.GetDriver.FindElement(By.Id("quantity_wanted")).Clear();
            BrowsersFactory.GetDriver.FindElement(By.Id("quantity_wanted")).SendKeys(quantity_value);

            SelectElement oSelect = new SelectElement(BrowsersFactory.GetDriver.FindElement(By.Id("group_1")));
            oSelect.SelectByText(size_value);


            BrowsersFactory.GetDriver.FindElement(By.XPath("//ul[@id='color_to_pick_list']//li//*[@name='" + color_value + "']")).Click();
        }
    }
}
