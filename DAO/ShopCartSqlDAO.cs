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
    // TODO: Get rid of Id property for product altogether? (is it necessary - name is already unique)
    public class ShopCartSqlDAO: IShopCartDAO
    {
        // Modifyable cart list
        private List<Product> cart = new List<Product>();

        // Non-modifyable selection from database
        private List<Product> selection = new List<Product>();

        private readonly string connectionString;

        // Monitor if cart is empty (if true, user can leave without going to checkout)
        private bool isCartEmpty = true;

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


        // Adds a product corresponding to the name from database, with its indicated amount, to the cart
        public void AddToCart(Product_Transfer productToAdd)
        {
            // If item is already in the cart, just increment the "amount" property 
            Product cartProduct = cart.SingleOrDefault(x => x.Name == productToAdd.Name);

            if (cartProduct != null)
            {
                cartProduct.Amount += productToAdd.Amount;
            } 

            // Else, add the selection to cart 
            else {

            // Get corresponding item from selection
             Product product = GetProductFromSelection(productToAdd);

             cart.Add(product);

             // Cart is not empty, so set to false
             isCartEmpty = false;

            }

        }

        // Decreases item's amount property / removes item from cart 
        public void RemoveFromCart(Product_Transfer productToRemove)
        {
            // Get corresponding cart item
            Product product = GetProductFromCart(productToRemove);

            // If amount param is less than amount property, decrement by param value 
            if (product.Amount > productToRemove.Amount)
            {
                product.Amount -= productToRemove.Amount;
            }

            // else, if amount to decrease is equal to amount property, remove the item from cart 
            else if (product.Amount == productToRemove.Amount)
            {
                cart.Remove(product);

                // Check if user has removed everything from cart
                if (cart == null)
                {
                    isCartEmpty = true;

                }

            }

            // else, if amount to decrease is greater than amount property, display error message
            else if (product.Amount < productToRemove.Amount)
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


        // Get product from selection list (used to add to cart)
        private Product GetProductFromSelection(Product_Transfer productMod)
        {
            Product productFromSelection = selection.SingleOrDefault(x => x.Name == productMod.Name);

            return productFromSelection;
        }

        // Get product from cart list (used to remove from cart)
        private Product GetProductFromCart(Product_Transfer productMod)
        {
            Product productFromSelection = cart.SingleOrDefault(x => x.Name == productMod.Name);

            return productFromSelection;
        }






    }
}
