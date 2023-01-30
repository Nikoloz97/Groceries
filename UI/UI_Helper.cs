using RefactorDemo.DAO;
using RefactorDemo.Models;
using RefactorDemo.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RefactorDemo.UI
{
    // Ensure user is putting in proper values
    public class UI_Helper
    {
        private readonly IShopCartDAO shoppingCartDao;

        public UI_Helper(IShopCartDAO shoppingCartDao)
        {
            this.shoppingCartDao = shoppingCartDao;
        }

        /*public string ProperCartItemName (string userInput)
        {
            // Don't know why this is null... 
            List<Product> cart = shoppingCartDao.GetCart();

            Product cartItemName = cart.SingleOrDefault(product => product.Name == userInput);   

            while (cartItemName == null) {

                Console.WriteLine("Invalid item name. Try again: ");
                userInput = Console.ReadLine();
                cartItemName = cart.SingleOrDefault(product => product.Name == userInput);
            }

            return cartItemName.Name;
        }
*/
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

    }
}
