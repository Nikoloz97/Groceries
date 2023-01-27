using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using RefactorDemo.Models;

namespace RefactorDemo.DAO
{
    public class ShoppingCart
    {
        // Create a list of products
        private List<Product> _products = new List<Product>();



        public decimal GetCheckoutPrice()
        {
            decimal price = 0;

            foreach (Product p in _products)
                price += p.Price;

            return price;
        }

        public void AddProduct(string name)
        {

            string dbPath = new DirectoryInfo(@"..\..").FullName + @"\GroceryStore.mdf";
            string connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}", dbPath);
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Product WHERE Name = '{0}'", name), conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Product p = null;

            if (dr.Read())
            {
                _products.Add(new Product
                {
                    Id = dr.GetFieldValue<int>("Id"),
                    Name = dr.GetFieldValue<string>("Name"),
                    Price = dr.GetFieldValue<decimal>("Price")
                });
            }
        }

       
    }
}
