using RefactorDemo.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace RefactorDemo
{
    // Main user interface 
    internal class UI_Main
    {
        private readonly IShopCartDAO shoppingCartDao;

        // Constructor
        public UI_Main(IShopCartDAO shoppingCartDao)
        {
            this.shoppingCartDao = shoppingCartDao;
        }


        // TODO: Complete switch statement
        public void RunMainMenu()
        {
            // Main menu display
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Welcome to Nick's grocery store!");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("1 - See groceries listing");
            Console.WriteLine("2 - See current checkout price");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();

            Console.Write("What's your choice?: ");

            int selection = Convert.ToInt32(Console.ReadLine());

            switch (selection)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Console.Clear();
                    // TODO: Add groceries listing
                    break;

                case 2:
                    Console.Clear();
                    decimal checkoutPrice = shoppingCartDao.GetCheckoutPrice();
                    Console.WriteLine($"Your current checkout price: ${checkoutPrice}");
                    break;
            }


        }



    }
}
