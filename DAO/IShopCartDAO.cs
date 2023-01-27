using RefactorDemo.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace RefactorDemo.DAO
{
    public interface IShopCartDAO
    {
        // Gets a product from database
        Product_Selection GetProduct(int productId);

        decimal GetCheckoutPrice();


        // Adds product to database
        void AddProduct(Product_Selection product);

        void AddProductToList(string name);

        // Updates product from database
        void UpdateProduct(int productId);

        // Deletes product from database
        void DeleteProduct(int productId);


    }
}
