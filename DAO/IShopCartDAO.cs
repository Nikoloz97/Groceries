using RefactorDemo.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace RefactorDemo.DAO
{
    // Reason its doesn't include sql in interface name = want it to be applied in non-sql DAOs as well 
    public interface IShopCartDAO
    {
        bool GetIsCartEmpty();
        public Product GetProductFromCart(string productName);

        List<Product> GetCart();

        decimal GetCheckoutPrice();

        List<Product> GetSelection();


        // Add item or item's amount property to cart
        void AddToCart(Product_Transfer product);

        //Remove item or item's amount property to cart
        void RemoveFromCart(Product_Transfer product);


    }
}
