using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreAutomation.Controller
{
    public class Validate
    {
        public static String[] getConsole = null;
        public static String[] Consoles
        {
            get { return getConsole; }
        }

        public static void ValidateShoppingCartSummary(string scenario)
        {
            DataTable oderTable = ExcelDataAccess.ExcelToDataTable("DataDriven\\OrdersData.xlsx", scenario);

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
