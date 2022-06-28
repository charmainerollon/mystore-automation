using MyStoreAutomation.Controller;
using MyStoreAutomation.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreAutomation
{
    [TestFixture]
    class Order : SetupTearDown
    {
        [Test, Order(0)]
        public void Order_01_GoToMainPage()
        {
            BrowsersFactory.GoToMainPage();
            HomePage.WaitPageTToLoad(10000, "My Store");
        }

        [Test, Order(1)]
        public void Order_02_AddOrder()
        {
            Search.SearchBasket("AddToBasket");
        }

        [Test, Order(2)]
        public void Order_03_ProceedToCheckout()
        {
            OrderPage.ProceedToCheckout();
        }

        [Test, Order(3)]
        public void Order_04_Validate_ShoppingCartSummary()
        {
            try
            {
                Validate.ValidateShoppingCartSummary("AddToBasket");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, HomePage.Consoles));
            }
        }

        [Test, Order(4)]
        public void Order_05_Edit_ShoppingCartSummary()
        {
           CheckoutPage.EditShoppingCartSummary("3");            
        }

        [Test, Order(5)]
        public void Order_06_Validate_Edit_ShoppingCartSummary()
        {
            try
            {
                Validate.ValidateShoppingCartSummary("EditToBasket");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, HomePage.Consoles));
            }            
        }
    }
}
