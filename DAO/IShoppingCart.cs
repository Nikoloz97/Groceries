using RefactorDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorDemo.DAO
{
    public interface IShoppingCart
    {
        // Gets a product from database
        Product GetProduct(int productId);

        // Adds a product to database
        void AddProduct(string name);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
