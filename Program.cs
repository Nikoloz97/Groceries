using RefactorDemo.DAO;
using RefactorDemo.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace RefactorDemo
{
    public static class Program
    {
        public static void Main()
        {
            // Path to database
            string dbPath = new DirectoryInfo(@"..\..").FullName + @"\GroceryStore.mdf";
            // Connection string
            string connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}", dbPath);

            // Place connection string -> DAO constructor
            IShopCartDAO shoppingCartDao = new ShopCartSqlDAO(connectionString);

            // Call main UI constructor
            UI_Main uiMain = new UI_Main(shoppingCartDao);

            // Run UI's main menu
            uiMain.MainMenu();

        }
    }
}