using RefactorDemo.DAO;
using RefactorDemo.Models;
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
        public void MainMenu()
        {
            // Main menu display
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Welcome to Nick's grocery store!");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("1 - See groceries menu");
            Console.WriteLine("2 - See what's in your cart");
            Console.WriteLine("3 - Go to checkout");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();

            Console.Write("What's your choice?: ");

            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Console.Clear();
                    GroceryDisplayMain();
                    Console.WriteLine();
                    GroceryDisplayOptions();
                    break;

                case 2:
                    Console.Clear(); 
                    DisplayCart();
                    Console.WriteLine();
                    DisplayCheckout();
                    Console.WriteLine();
                    DisplayCartOptions();
                    break;
            }
        }

        public void GroceryDisplayMain()
        {
            List<Product> selection = shoppingCartDao.GetSelection();
            Console.WriteLine("----------------------");
            Console.WriteLine("| Nick's Grocery Menu |");
            Console.WriteLine("----------------------");
            Console.WriteLine("|   Name   |   Price  |");

            foreach (Product product in selection)
            {
                Console.WriteLine($"|   {product.Name}   |   {product.Price}  |");
            }
            Console.WriteLine("------------------");

        }

        public void GroceryDisplayOptions()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - Add item to cart");
            Console.WriteLine("2 - Return to main menu");

            int userinput = Convert.ToInt32(Console.ReadLine());  

            switch (userinput) 
            {
                case 1:
                    AddToCart();
                    GenericMenu();
                    break;
                case 2:
                    Console.Write("What would you like to remove?: ");
                    string itemToRemove = Console.ReadLine();
                    C
                    break;
            }
        }

        public void AddToCart()
        {
            string continueAdding;

            do
            {
                Console.Write("What would you like to add?: ");
                string itemToAdd = Console.ReadLine();

                Console.Write("How many?: ");
                int amountToAdd = Convert.ToInt32(Console.ReadLine());

                shoppingCartDao.AddToCart(itemToAdd, amountToAdd);

                Console.Write("Would you like to add anything else? (y/n) ");
                continueAdding = Console.ReadLine().ToLower();

            } while (continueAdding == "y");

         
        }

        public void GenericMenu()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - go to cart/checkout");
            Console.WriteLine("2 - go to main menu");

            int userInput = Convert.ToInt32(Console.ReadLine());    

            switch (userInput) { 
                case 1: 
                    DisplayCart();
                    Console.WriteLine();
                    DisplayCheckout();
                    Console.WriteLine();
                    DisplayCartOptions();
                    break;
                case 2:
                    MainMenu();
                    break;
            }
           
        }

        public void DisplayCart()
        {
            List<Product> cart = shoppingCartDao.GetCart();

            Console.WriteLine("Your Cart");
            Console.WriteLine("|Name -- Price -- Amount|");

            foreach (Product product in cart)
            {
                Console.WriteLine($"|{product.Name} | {product.Price} | {product.Amount}|");
            }

        }

        public void DisplayCheckout()
        {
            decimal currentTotal = shoppingCartDao.GetCheckoutPrice();

            Console.WriteLine($"Your current total: {currentTotal}");
        }

        public void DisplayCartOptions()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - Add new items to cart (go to groceries menu)");
            Console.WriteLine("2 - Add more of an item");
            Console.WriteLine("3 - Remove item from cart");
            Console.WriteLine("4 - Go to checkout");

            Console.WriteLine();

            Console.WriteLine("What's your choice?: ");

            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput) {
                case 1:
                    MainMenu();
                    break;
                case 2:

                    Console.WriteLine("What item would you like to increase the amount of?: ");
                    string itemToIncrease = Console.ReadLine();

                    Console.WriteLine("How much would you like to add?: ");
                    int addAmount = Convert.ToInt32(Console.ReadLine());

                    RemoveFromCart(itemToIncrease, addAmount);

                    break;
                case 3:

                    Console.WriteLine("What item would you like to decrease the amount of?: ");
                    string itemToDecrease = Console.ReadLine();

                    Console.WriteLine("How much would you like to add?: ");
                    int decAmount = Convert.ToInt32(Console.ReadLine());

                    AddToCart(itemToDecrease, decAmount);

                    break;
                case 4:
                    CheckoutDisplay();
                    break;

            }

        }

        public void AddToCart(string itemName, int addAmount)
        {
            shoppingCartDao.AddToCart(itemName, addAmount); 

        }

        public void RemoveFromCart(string itemName, int addAmount)
        {
            shoppingCartDao.RemoveFromCart(itemName, addAmount);
        }

        public void CheckoutDisplay()
        {
            decimal currentTotal = shoppingCartDao.GetCheckoutPrice();
            Console.WriteLine($"Your final total: {currentTotal}");
        }

     
    }
}
