using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorDemo.Models
{
    public class Product
    {
        // Created getters/setters
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Amount property = just used for when displaying the items to user (see UI_Main) 
        public int Amount { get; set; }
    }
}
