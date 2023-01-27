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

        public void GetProduct()
        {
            
        }

        // For each product in product list, adds price properties and returns a sum of product prices
        public decimal GetCheckoutPrice()
        {
            decimal price = 0;

            foreach (Product p in _products)
                price += p.Price;

            return price;
        }


        // Adds a product with corresponding name from database to the product list
        public void AddProduct(string name)
        {
            // Path to database
            string dbPath = new DirectoryInfo(@"..\..").FullName + @"\GroceryStore.mdf";
            // Connection string
            string connectionString = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0}", dbPath);
            SqlConnection conn = new SqlConnection(connectionString);

            // Grab product with corresponding name from database
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Product WHERE Name = '{0}'", name), conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

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

        public void UpdateProduct()
        {

        }

        public void DeleteProduct()
        {

        }



       
    }
}
