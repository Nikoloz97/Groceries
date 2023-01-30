using RefactorDemo.DAO;
using RefactorDemo.Models;
using RefactorDemo.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace RefactorDemo
{
    // Main user interface 
    public class UI_Main
    {
        private readonly IShopCartDAO shoppingCartDao;
        readonly UI_Helper helper;


        // Constructor
        public UI_Main(IShopCartDAO shoppingCartDao)
        {
            this.shoppingCartDao = shoppingCartDao;
            this.helper = new UI_Helper(this.shoppingCartDao);
        }


        public void MainMenu()
        {
            // Main menu display
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Welcome to Nick's grocery store!");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("1 - Groceries menu");
            Console.WriteLine("2 - Cart/Checkout");
            DisplayFastExitOption();
            Console.WriteLine();

            Console.Write("What's your choice?: ");

            string userInput = Console.ReadLine();

            int properInputVal;

            if (shoppingCartDao.GetIsCartEmpty())
            {
                // Fast exit option (empty cart)
                properInputVal = ProperValChecker(userInput, 3);
            }
            else
            {
                // No fast exit option (cart not empty)
                properInputVal = ProperValChecker(userInput, 2);
            }

 
            switch (properInputVal)
            {
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
                case 3:
                    Console.Clear();
                    DisplayExit();
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
            Console.WriteLine("-----------------------");

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
            Console.WriteLine();
            Console.Write("What's your choice?: ");

            string userInput = Console.ReadLine();

            int properUserInput = ProperValChecker(userInput, 2);
            
            switch (properUserInput) 
            {
                case 1:
                    AddToCartPrompt();
                    GenericMenu();
                    break;
                case 2:
                    MainMenu();
                    break;
            }
        }

        public void AddToCartPrompt()
        {
            string properYesNo;

            do
            {
                Product_Transfer productToAdd = new Product_Transfer();

                Console.Write("What would you like to add?: ");
                string itemName = Console.ReadLine();
                // Helper problem here
                string properItemName = helper.ProperNameChecker_Selection(itemName);

                productToAdd.Name = properItemName;


                Console.Write("How many?: ");
                int amountToAdd = Convert.ToInt32(Console.ReadLine());

                productToAdd.Amount = amountToAdd;

                shoppingCartDao.AddToCart(productToAdd);

                Console.Write("Would you like to add anything else? (y/n) ");
                string yesNo = Console.ReadLine();
                properYesNo = ProperYesNoChecker(yesNo);


            } while (properYesNo == "y");

         
        }

        public void RemoveFromCartPrompt()
        {
            string properYesNo;

            do
            {
                Product_Transfer productToRemove = new Product_Transfer();

                Console.Write("What would you like to remove?: ");
                string itemToRemove = Console.ReadLine();

                string properItemToRemove = ProperNameChecker_Selection(itemToRemove);

                productToRemove.Name = properItemToRemove;


                Console.Write("How many?: ");
                string amountToRemove = Console.ReadLine();
                int properAmountToRemove = ProperToRemoveChecker(amountToRemove, productToRemove.Name);

                productToRemove.Amount = properAmountToRemove;

                shoppingCartDao.RemoveFromCart(productToRemove);

                Console.Write("Would you like to remove anything else? (y/n) ");
                string yesNo = Console.ReadLine();
                properYesNo = ProperYesNoChecker(yesNo);


            } while (properYesNo == "y");

        }

        public void GenericMenu()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - Cart/Checkout");
            Console.WriteLine("2 - Main Menu");

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
            Console.WriteLine("1 - Add new item (Groceries menu)");
            Console.WriteLine("2 - Add more of an item");
            Console.WriteLine("3 - Decrease/remove item from cart");
            Console.WriteLine("4 - Go to checkout");

            Console.WriteLine();

            Console.WriteLine("What's your choice?: ");

            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput) {
                case 1:
                    GroceryDisplayMain();
                    GroceryDisplayOptions();
                    break;
                case 2:
                    AddToCartPrompt();
                    break;
                case 3:
                    RemoveFromCartPrompt();
                    break;
                case 4:
                    CheckoutDisplay();
                    break;

            }

        }

        public void AddToCart(Product_Transfer productToAdd)
        {
            shoppingCartDao.AddToCart(productToAdd); 

        }

        public void RemoveFromCart(Product_Transfer productToRemove)
        {
            shoppingCartDao.RemoveFromCart(productToRemove);
        }

        public void CheckoutDisplay()
        {
            decimal currentTotal = shoppingCartDao.GetCheckoutPrice();
            Console.WriteLine($"Your final total: {currentTotal}");
        }


       /* public void ExitWithoutCheckout()
        {
            decimal checkoutPrice = shoppingCartDao.GetCheckoutPrice();

            if (checkoutPrice == 0)
            {
                DisplayExit();
            }
        }*/


        // User can leave if their checkout price is zero
        public void DisplayFastExitOption()
        {
            if (shoppingCartDao.GetIsCartEmpty())
            {
                Console.WriteLine("3 - Don't have anything in the cart? Exit here");
            }

        }

        public void DisplayExit()
        {
            Console.WriteLine("Thanks for choosing Nick's Grocery store - come again!");
            Thread.Sleep(300);
            Environment.Exit(0);
        }




        // TODO: Move everything below to UI_Helper (once null situation straightened out...) 


        // Checks if user's cart name input is valid
        public string ProperNameChecker_Cart(string userInput)
        {
            
            List<Product> cart = shoppingCartDao.GetCart();

            Product cartItem = cart.SingleOrDefault(product => product.Name == userInput);

            while (cartItem == null)
            {

                Console.Write("Invalid item name. Try again: ");
                userInput = Console.ReadLine();
                cartItem = cart.SingleOrDefault(product => product.Name == userInput);
            }

            return cartItem.Name;
        }

        // Checks if user's selection name input is valid
        public string ProperNameChecker_Selection(string userInput)
        {

            List<Product> selection = shoppingCartDao.GetSelection();

            Product selectionItem = selection.SingleOrDefault(product => product.Name == userInput);

            while (selectionItem == null)
            {
                Console.Write("Invalid item name. Try again: ");
                userInput = Console.ReadLine();
                selectionItem = selection.SingleOrDefault(product => product.Name == userInput);
            }

            return selectionItem.Name;
        }




        // Checks if user's number input is valid 
        public int ProperValChecker(string userInput, int upperRangeInclusive)
        {
            bool isProperValue = false;
            int parsedValue = 0;

            while (!isProperValue)
            {
                bool isParsed = int.TryParse(userInput, out parsedValue);

                if (!isParsed)
                {
                    Console.Write("Input was not a number. Please try again: ");
                    userInput = Console.ReadLine();
                }
                else if (parsedValue > upperRangeInclusive)
                {
                    Console.Write("Input was not a valid number. Please try again: ");
                    userInput = Console.ReadLine();
                }
                else
                {
                    isProperValue = true;
                }

            }

            return parsedValue;

        }

        public string ProperYesNoChecker(string userInput)
        {
            bool validUserInput = false;

            do
            {
                if (userInput.ToLower() != "y" && userInput.ToLower() != "n")
                {
                    Console.Write("Not a valid input. Try again: ");
                    userInput = Console.ReadLine();
                }
                else
                {
                    validUserInput = true;
                }

            } while (!validUserInput);

            return userInput;
        }

        public int ProperToRemoveChecker(string amountToRemove, string productName)
        {
            bool isProperValue = false;
            int parsedValue = 0;
            Product product = shoppingCartDao.GetProductFromCart(productName);

            while (!isProperValue)
            {
                bool isParsed = int.TryParse(amountToRemove, out parsedValue);

                if (!isParsed)
                {
                    Console.Write("Input was not a number. Please try again: ");
                    amountToRemove = Console.ReadLine();
                }
                else if (parsedValue > product.Amount)
                {
                    Console.Write("Input was too high of a number. Please try again: ");
                    amountToRemove = Console.ReadLine();
                }
                else
                {
                    isProperValue = true;
                }

            }

            return parsedValue;
        }




    }
}
