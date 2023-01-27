using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using RefactorDemo.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RefactorDemo.DAO
{
    public class ShopCartDAO: IShopCartDAO
    {
        // Modifyable cart 
        private List<Product> cart = new List<Product>();

        // selection of all items in the database 
        private List<Product> selection = new List<Product>();

        private readonly string connectionString;

        // Constructor (see program file) 
        public ShopCartDAO(string connString)
        {
            connectionString = connString;
        }

         
    
        public List<Product> GetCart()
        {
            return cart;
        }

        // For each product in product list, adds price properties and returns a sum of product prices
        public decimal GetCheckoutPrice()
        {
            decimal price = 0;

            foreach (Product p in cart)
                price += p.Price;

            return price;
        }

        // Provides full selection of products from database
        public List<Product> GetSelection()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product", conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Product product = CreateProductFromReader(dr);
                    selection.Add(product);
                }
            }
            return selection;
        }


        // Adds a product corresponding to the name from database, and indicated amount, to the cart
        public void AddToCart(string name, int amount)
        {
            // If item is already in the cart, just increment the "amount" property 
            Product productToAdd = cart.SingleOrDefault(x => x.Name == name);
            if (productToAdd != null)
            {
                productToAdd.Amount += amount;
            } 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Name = @name;", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Product product = CreateProductFromReader(dr);
                    product.Amount = amount;
                    cart.Add(product);
                }

            };
        }

        // Decreases the amount of an item from the cart
        public void DecreaseFromCart(int productId, int amount)
        {
            Product productToDec = cart.SingleOrDefault(x => x.Id == productId);
            if (productToDec != null && productToDec.Amount > amount) {
                productToDec.Amount -= amount;
            }
            else if (pro)
            else if (productToDec != null)
            {
                Console.WriteLine("Woops, there is not item by that amount");
            }

        }


        // Deletes a product from cart
        public void DeleteFromCart(int productId)
        {
            Product productToRemove = cart.SingleOrDefault(x => x.Id == productId);
            if (productToRemove != null) { cart.Remove(productToRemove); }
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
