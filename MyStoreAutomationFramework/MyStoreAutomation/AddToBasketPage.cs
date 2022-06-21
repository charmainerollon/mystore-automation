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
    public class AddToBasketPage
    {
        public static String[] getConsole = null;
        public static String[] Consoles
        {
            get { return getConsole; }
        }

        public static void WaitPageTToLoad(int timeout, string pageTitle)
        {
            BrowsersFactory.GetDriver.Wait(timeout).ForPage().ReadyStateComplete();
            BrowsersFactory.GetDriver.Wait(timeout).ForPage().TitleToEqual(pageTitle);
        }

        public static void SearchOrder(string search_value)
        {
            BrowsersFactory.GetDriver.FindElement(By.Id("search_query_top")).SendKeys(search_value);
            BrowsersFactory.GetDriver.FindElement(By.Name("submit_search")).Click();
            AddToBasketPage.WaitPageTToLoad(10000, "Search - My Store");
        }

        public static void SelectOrder(string price_value, string color_value, string size_value, string quantity_value)
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'product-price')][contains(text(), '" + price_value + "')]//ancestor::*[contains(@class, 'right-block')]//*[contains(@class, 'available-now')]")).Click();
            Thread.Sleep(2000);
            BrowsersFactory.GetDriver.FindElement(By.XPath(" //*[contains(@class, 'product-price')][contains(text(), '" + price_value + "')]//ancestor::*[contains(@class, 'right-block')]//*[text() ='More']")).Click();
            AddToBasketPage.WaitPageTToLoad(10000, "Printed Summer Dress - My Store");

            BrowsersFactory.GetDriver.FindElement(By.Id("quantity_wanted")).Clear();
            BrowsersFactory.GetDriver.FindElement(By.Id("quantity_wanted")).SendKeys(quantity_value);

            SelectElement oSelect = new SelectElement(BrowsersFactory.GetDriver.FindElement(By.Id("group_1")));
            oSelect.SelectByText(size_value);
            

            BrowsersFactory.GetDriver.FindElement(By.XPath("//ul[@id='color_to_pick_list']//li//*[@name='" + color_value + "']")).Click();
        }

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

            AddToBasketPage.WaitPageTToLoad(10000, "Order - My Store");
        }

        public static void SearchBasket(string scenario)
        {
            DataTable oderTable = ExcelDataAccess.ExcelToDataTable("OrdersData.xlsx", scenario);

            foreach (DataRow orderRow in oderTable.Rows)
            {
                string search_value = orderRow["ProductName"].ToString();
                string price_value = orderRow["PriceValue"].ToString();
                string color_value = orderRow["Color"].ToString();
                string size_value = orderRow["Size"].ToString();
                string quantity_value = orderRow["Quantity"].ToString();

                SearchOrder(search_value);
                SelectOrder(price_value, color_value, size_value, quantity_value);
                AddToCart();
            }          
        }          
        
        public static void EditShoppingCartSummary(string qtyValue)
        {
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_quantity_input')]")).Clear();
            BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_quantity_input')]")).SendKeys(qtyValue);

            Thread.Sleep(10000);
        }

        public static void ValidateShoppingCartSummary(string scenario)
        {
            DataTable oderTable = ExcelDataAccess.ExcelToDataTable("OrdersData.xlsx", scenario);

            List<string> list = new List<string>();
            List<string> cwList = new List<string>();

            foreach (DataRow orderRow in oderTable.Rows)
            {
                string product_name_value = orderRow["ProductName"].ToString();
                string color_value = orderRow["Color"].ToString();
                string size_value = orderRow["Size"].ToString();


                string tolal_product_value = orderRow["TotalProduct"].ToString();
                string total_shipping_value = orderRow["TotalShipping"].ToString();
                string totalpricewithouttax_value = orderRow["TotalPriceWithoutTax"].ToString();
                string totaltax_value = orderRow["TotalTax"].ToString();
                string totalprice_value = orderRow["TotalPrice"].ToString();

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_description')]//*[contains(@class, 'product-name')]//*[contains(text(), '" + product_name_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//table[contains(@class, 'annual-adjustments-total-table')]//*"));

                    cwList.Add("Failed! Incorrect value in ProductName!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + product_name_value + "");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_description')]//*[contains(text(), 'Color : " + color_value + ", Size : " + size_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[contains(@class, 'cart_description')]//*"));

                    cwList.Add("Failed! Incorrect value in Color and Size!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => Color : " + color_value + ", Size : " + size_value + "')]");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_product'][contains(text(), '" + tolal_product_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_product']"));

                    cwList.Add("Failed! Incorrect value in Total Product!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + tolal_product_value + "");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_shipping'][contains(text(), '" + total_shipping_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_shipping']"));

                    cwList.Add("Failed! Incorrect value in Total Shipping!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + total_shipping_value + "");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_price_without_tax'][contains(text(), '" + totalpricewithouttax_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_price_without_tax']"));

                    cwList.Add("Failed! Incorrect value in Total Price Without Tax!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + totalpricewithouttax_value + "");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_tax'][contains(text(), '" + totaltax_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_tax']"));

                    cwList.Add("Failed! Incorrect value in Total Tax!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + totaltax_value + "");
                    list.Add("Failed");
                }

                try { BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_price'][contains(text(), '" + totalprice_value + "')]")); }
                catch (NoSuchElementException)
                {
                    IWebElement value = BrowsersFactory.GetDriver.FindElement(By.XPath("//*[@id ='total_price']"));

                    cwList.Add("Failed! Incorrect value in Total Price!. Actual Result => " + value.GetAttribute("textContent") + ". Expected Result => " + totalprice_value + "");
                    list.Add("Failed");
                }
            }

            String[] resultStr = list.ToArray();

            getConsole = cwList.ToArray();
            if (resultStr.Contains("Failed"))
            {
                Assert.Fail("Failed Shopping Cart Summary! See the following.");
            }

        }

    }
}


