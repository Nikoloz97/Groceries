using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RefactorDemo.Models
{
    // Used for when altering product (adding product to list, incrementing amount property, etc.) 
    public class Product_Transfer
    {
        // This is to match with corresponding Product
        public string Name { get; set; }

        // This is to increment/decrement corresponding Product
        public int Amount { get; set; }


        // Constructor for unit tests
        public Product_Transfer(string name, int amount)
        {
            this.Name = name;
            this.Amount = amount;
        }

        public Product_Transfer(string name)
        {
            this.Name = name;
        }

        public Product_Transfer()
        {
        }
    }
}
