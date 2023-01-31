# Refactor Demo Instructions

This code represents some tight coupling and poor programming practices. We would like you to do some refactoring and code clean-up.

Examples of refactors you could make include (this is not an exhaustive list):
-Implementing a data repository and using dependency injection to provide it where needed.
-Altering the AddProduct() method to take in an object.
-Ensuring that objects are properly cleaned up where appropriate.
-Implementing defensive coding checks to provide feedback to the user.
-Making the GetCheckoutPrice() method in the ShoppingCart class testable by a unit test.
-BONUS: Write a unit test against the GetCheckoutPrice() method without accessing the database.
The overall objective of this exercise is to gauge your level of understanding and creativity. If you aren't sure how to act on some of the suggestions that is ok. If you find other things you want to change to make the code cleaner and easier to maintain feel free to do so.

# Refactor Demo Update (1/31/2023)

## General

Created an application called "Nick's Grocery Store" where a user can navigate between main, grocery, cart, and checkout screens. At the grocery menu, user can: 1. see a selection of items (gathered from a SQL database), and 2. Add items to their virtual "cart". In the cart menu, user can: 1. see their cart items and current total, 2. Add more of an item currently in their cart, and 3. Decrease amount or remove item from cart. Finally, in the checkout screen, user has an option to pay their final total either by cash or card, and is taken to a processing/exit screen.

Overall solution organization: Program.cs, Logger.cs, 3 directories (DAO, Models, UI). Program file holds the connectionString and begins running the program. DAO directory holds ShopCartSqlDAO and interface, Models directory holds Products (as found in cart/selection) and Product_Transfer (as created by userInput) classes, and UI directory holds the user interface, where most of the defensive coding checks are found.

## Objectives completed:

- Altering the AddProduct() method to take in an object
- Ensuring that objects are properly cleaned up where appropriate
- Making the GetCheckoutPrice() method in the ShoppingCart class testable by a unit test
- Write a unit test against the GetCheckoutPrice() method without accessing the database
- Implementing defensive coding checks to provide feedback to the user

## Notes:

- Changed name of AddProduct() -> AddToCart()

## Todos:

- Learn more about repository pattern (Entity framework)
- Learn more about integration testing via xUnit testing (see Database Fixture file in tests)
- Create admin option (can modify SQL database directly)
- Complete moving functions from UI_main -> UI_helper (improve level of abstraction)
- Create logger (produces a .txt file of user history (i.e. adding/removing items from cart))

## Takeaways:

- Start unit/integration tests first. I would refactor multiple methods so they would be more test-friendly
