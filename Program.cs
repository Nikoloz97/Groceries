using RefactorDemo.DAO;
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

            // Place connection string DAO constructor
            IShopCartDAO shoppingCartDao = new ShopCartDAO(connectionString);

            // Hard coded these products (to see if it works) 
            shoppingCartDao.AddProductToList("Milk");
            shoppingCartDao.AddProductToList("Eggs");


            UI_Main ui = new UI_Main(shoppingCartDao);

            ui.MainMenu();

        }
    }
}