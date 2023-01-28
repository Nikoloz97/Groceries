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
    public class ShopCartSqlDAO: IShopCartDAO
    {
        // Modifyable cart list
        private List<Product> cart = new List<Product>();

        // Non-modifyable selection from database
        private List<Product> selection = new List<Product>();

        private readonly string connectionString;

        // Constructor (see program file) 
        public ShopCartSqlDAO(string connString)
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

            foreach (Product product in cart)
                price += (product.Price * product.Amount);

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
            Product productToAdd = cart.SingleOrDefault(product => product.Name == name);
            if (productToAdd != null)
            {
                productToAdd.Amount += amount;
            } 

            // Else, add to cart 
            // TODO: get rid of sql command? We already have the selection
            else { 
            
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

        }

        // Decreases item's amount property / removes item from cart 
        public void RemoveFromCart(string productName, int amount)
        {
            Product product = cart.SingleOrDefault(product => product.Name == productName);

            // If amount param is less than amount property, decrement by param value 
            if (product.Amount > amount)
            {
                product.Amount -= amount;
            }

            // else, if amount to decrease is equal to amount property, remove the item from cart 
            else if (product.Amount == amount)
            {
                cart.Remove(product);

            }

            // else, if amount to decrease is greater than amount property, display error message
            else if (product.Amount < amount)
            {
                Console.WriteLine("Woops - you can't decrease by more than what's in your cart. Try again.");
            }

            // else, if product cannot be found, display error message
            else if (product == null)
            {
                Console.WriteLine("Woops - couldn't find item. Try again.");
            }
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
