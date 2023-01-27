using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using RefactorDemo.Models;

namespace RefactorDemo.DAO
{
    public class ShopCartDAO: IShopCartDAO
    {
        // Create a list of products
        private List<Product> productsList = new List<Product>();

        private readonly string connectionString;

        // Constructor (see program file) 
        public ShopCartDAO(string connString)
        {
            connectionString = connString;
        }

         

        public Product GetProduct(int productId)
        {
            return null;
        }

        // For each product in product list, adds price properties and returns a sum of product prices
        public decimal GetCheckoutPrice()
        {
            decimal price = 0;

            foreach (Product p in productsList)
                price += p.Price;

            return price;
        }

        // Add product to database
        public void AddProduct(Product p)
        {

        }


        // Adds a product with corresponding name from database to the product list
        public void AddProductToList(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Name = @name;", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Product product = CreateProductFromReader(dr);
                    productsList.Add(product);
                }

            };
        }





        // Updates a product 
        public void UpdateProduct(int productId)
        {

        }

        // Deletes a product
        public void DeleteProduct(int productId)
        {

        }


        private Product CreateProductFromReader(SqlDataReader dr)
        {
            Product product = new Product();

            product.Id = Convert.ToInt32(dr["Id"]);
            product.Name = Convert.ToString(dr["Name"]);
            product.Price = Convert.ToDecimal(dr["Price"]);

            return product;
        }



       
    }
}
