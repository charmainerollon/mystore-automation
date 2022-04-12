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
            AddToBasketPage.WaitPageTToLoad(10000, "My Store");
        }

        [Test, Order(1)]
        public void Order_02_AddOrder()
        {
            AddToBasketPage.SearchBasket("AddToBasket");
        }

        [Test, Order(2)]
        public void Order_03_ProceedToCheckout()
        {
            AddToBasketPage.ProceedToCheckout();
        }

        [Test, Order(3)]
        public void Order_04_Validate_ShoppingCartSummary()
        {
            try
            {
                AddToBasketPage.ValidateShoppingCartSummary("AddToBasket");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, AddToBasketPage.Consoles));
            }
        }

        [Test, Order(4)]
        public void Order_05_Edit_ShoppingCartSummary()
        {
           AddToBasketPage.EditShoppingCartSummary("3");            
        }

        [Test, Order(5)]
        public void Order_06_Validate_Edit_ShoppingCartSummary()
        {
            try
            {
                AddToBasketPage.ValidateShoppingCartSummary("EditToBasket");
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, AddToBasketPage.Consoles));
            }            
        }
    }
}
