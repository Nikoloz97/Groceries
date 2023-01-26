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
            var shoppingCart = new ShoppingCart();
            shoppingCart.AddProduct("Milk");
            shoppingCart.AddProduct("Eggs");
            Console.WriteLine("Total Price: {0}", shoppingCart.GetCheckoutPrice());
        }
    }
}