using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorDemo.Models
{
    internal class Product
    {
        public int Id;
        public string Name;
        public decimal Price;
        // Amount property = just used for when displaying the items to user (see UI_Main) 
        public int Amount = 0;
    }
}
