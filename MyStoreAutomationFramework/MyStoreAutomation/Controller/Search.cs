using MyStoreAutomation.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreAutomation.Controller
{
    public class Search
    {
        public static void SearchBasket(string scenario)
        {
            DataTable oderTable = ExcelDataAccess.ExcelToDataTable("DataDriven\\OrdersData.xlsx", scenario);

            foreach (DataRow orderRow in oderTable.Rows)
            {
                string search_value = orderRow["ProductName"].ToString();
                string price_value = orderRow["PriceValue"].ToString();
                string color_value = orderRow["Color"].ToString();
                string size_value = orderRow["Size"].ToString();
                string quantity_value = orderRow["Quantity"].ToString();

                HomePage.SearchOrder(search_value);
                OrderPage.SelectOrder(price_value, color_value, size_value, quantity_value);
                OrderPage.AddToCart();
            }
        }
    }
}
