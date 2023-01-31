using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorDemo.Models
{
    public class Product
    {
    
        // Products in the cart/selection list

        // Created getters/setters
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Amount property = just used for when displaying the items to user (see UI_Main) 
        public int Amount { get; set; }


        // Constructor for unit tests
        public Product(int id, string name, decimal price, int amount)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Amount = amount;
        }

        public Product()
        {
        }
    }
}
