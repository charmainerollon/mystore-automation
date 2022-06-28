using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStoreAutomation.Pages
{
    public class CheckoutPage
    {

        public static void EditShoppingCartSummary(string qtyValue)
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_quantity_input')]")).Clear();
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_quantity_input')]")).SendKeys(qtyValue);

            Thread.Sleep(10000);
        }

    }
}
