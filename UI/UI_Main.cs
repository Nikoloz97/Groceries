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
                    DisplayCurrentTotal();
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
            Console.WriteLine("---------------------");

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
                    Console.Clear();
                    GenericMenu();
                    break;
                case 2:
                    Console.Clear();
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
                string amountToAdd = Console.ReadLine();
                int properAmountToAdd = ProperValChecker(amountToAdd);


                productToAdd.Amount = properAmountToAdd;

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


            } while (properYesNo.ToLower() == "y");

        }

        public void GenericMenu()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - Cart/Checkout");
            Console.WriteLine("2 - Main Menu");
            Console.WriteLine("3 - Grocery Menu");
            Console.WriteLine();
            Console.Write("Your choice?: ");

            string userInput = Console.ReadLine();
            int properUserInput = ProperValChecker(userInput, 3);

            switch (properUserInput) { 
                case 1:
                    Console.Clear();
                    DisplayCart();
                    Console.WriteLine();
                    DisplayCurrentTotal();
                    Console.WriteLine();
                    DisplayCartOptions();
                    break;
                case 2:
                    Console.Clear();
                    MainMenu();
                    break;
                case 3:
                    Console.Clear();
                    GroceryDisplayMain();
                    GroceryDisplayOptions();
                    break;
            }
           
        }

        public void DisplayCart()
        {
            List<Product> cart = shoppingCartDao.GetCart();

            Console.WriteLine("------------------------------------");
            Console.WriteLine("|             Your Cart            |");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("|   Name   |   Price   |   Amount  |");
            Console.WriteLine("------------------------------------");

            foreach (Product product in cart)
            {
                Console.WriteLine($"|   {product.Name}   |   {product.Price}   |      {product.Amount}      |");
            }

        }

        public void DisplayCurrentTotal()
        {
            decimal currentTotal = shoppingCartDao.GetCheckoutPrice();

             Console.WriteLine("--------------------------------------");
            Console.WriteLine($"| Your current total: {currentTotal} |");
             Console.WriteLine("--------------------------------------");
        }

        public void DisplayCartOptions()
        {
            Console.WriteLine("Please choose from the following options: ");
            Console.WriteLine("1 - Add new item (groceries menu)");
            Console.WriteLine("2 - Increase amount of an item");
            Console.WriteLine("3 - Decrease/remove item from cart");
            Console.WriteLine("4 - Go to checkout");

            Console.WriteLine();

            Console.Write("What's your choice?: ");

            string userInput = Console.ReadLine();

            int properUserInput = ProperValChecker(userInput, 4);

            switch (properUserInput) {
                case 1:
                    Console.Clear();
                    GroceryDisplayMain();
                    GroceryDisplayOptions();
                    break;
                case 2:
                    AddToCartPrompt();
                    Console.Clear();
                    DisplayCart();
                    DisplayCurrentTotal();
                    DisplayCartOptions();
                    break;
                case 3:
                    RemoveFromCartPrompt();
                    Console.Clear();
                    DisplayCart();
                    DisplayCurrentTotal();
                    DisplayCartOptions();
                    break;
                case 4:
                    Console.Clear();
                    CheckoutDisplay();
                    CheckoutPrompts();
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
             Console.WriteLine("------------------------------------");
            Console.WriteLine($"| Your final total: {currentTotal} |");
             Console.WriteLine("------------------------------------");
        }

        public void CheckoutPrompts()
        {
            Console.WriteLine("Will you be paying by cash or card?");
            Console.WriteLine("1 - Cash");
            Console.WriteLine("2 - Card");
            string userInput = Console.ReadLine();
            int properUserInput = ProperValChecker(userInput, 2);
            switch (properUserInput)
            {
                case 1:
                    Console.Clear();
                    DisplayLoadingScreen();
                    Console.WriteLine("Success!");
                    Console.WriteLine();
                    DisplayExit();
                    break;
                case 2:
                    Console.Clear();
                    DisplayLoadingScreen();
                    Console.WriteLine("Success!");
                    Console.WriteLine();
                    DisplayExit();
                    break;
            }
        }


        // User can leave if their checkout price is zero
        public void DisplayFastExitOption()
        {
            if (shoppingCartDao.GetIsCartEmpty())
            {
                Console.WriteLine("3 - You currently don't have any items in your cart. Quick exit here");
            }
            
        }

        public void DisplayLoadingScreen()
        {
            int counter = 0;
            string processing = "Processing";

            while (counter < 4)
            {
                Console.Clear();
                processing +=  ".";
                Console.WriteLine($"{processing}");
                counter++;
                Thread.Sleep(1000);

            }
            
        }

        public void DisplayExit()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("| Thanks for choosing Nick's Grocery store - come again! |");
            Console.WriteLine("----------------------------------------------------------");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }




        // TODO: Move everything below to UI_Helper (once null situation straightened out...) 


        // Checks if user's cart name input is valid
        public string ProperNameChecker_Cart(string userInput)
        {
            
            List<Product> cart = shoppingCartDao.GetCart();

            Product cartItem = cart.SingleOrDefault(product => product.Name.ToLower() == userInput.ToLower());

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

            Product selectionItem = selection.SingleOrDefault(product => product.Name.ToLower() == userInput.ToLower());

            while (selectionItem == null)
            {
                Console.Write("Invalid item name. Try again: ");
                userInput = Console.ReadLine();
                selectionItem = selection.SingleOrDefault(product => product.Name == userInput);
            }

            return selectionItem.Name;
        }




        // Checks if user's number input is valid. UpperRangeInclusive = optional paramater
        public int ProperValChecker(string userInput, int upperRangeInclusive = Int32.MaxValue)
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
                else if (parsedValue <= 0)
                {
                    Console.WriteLine("Input was too low. Please try again");
                    userInput = Console.ReadLine();
                }
                else if (parsedValue > upperRangeInclusive)
                {
                    Console.Write("Input was not in range. Please try again: ");
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
