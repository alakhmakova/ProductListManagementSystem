# Product List Management System

A C# console application for managing a list of products. You can add products, see them in a table sorted by price with a total sum, and search by product name or category.

## Description

The app is built for a warehouse administrator who keeps track of products and their prices. It was written as a course assignment (Checkpoint 2) to practise object-oriented programming, LINQ, input validation and custom exceptions in C#.

The code is split into layers, so that each class has one job:

- **Models** – `Product` holds the data of one product and validates it in its constructor.
- **Services** – `ProductManager` stores the product list and does the work on it: adding, sorting, searching and calculating the total. It never writes to the console.
- **UI** – `ConsoleUI` shows the menu and the tables and catches errors; `InputHelper` reads and validates user input and prints colored messages.
- **Exceptions** – custom exceptions (`ValidationException`, `ProductsNotFoundException`, `UserCancelledException`, `InputClosedException`) carry error messages from the logic to the UI.

### Features

- **Add products** – enter category, name and price; keep adding until you type `Q` (works at any step).
- **Input validation** – empty category or name is rejected; the price must be a number greater than 0 with at most 2 decimals. On invalid input the app asks again instead of crashing.
- **List products** – all products in a table, sorted from lowest to highest price, with the total at the bottom. Column widths adjust to the longest value.
- **Search** – by part of the product name or category, case-insensitive (`mouse` finds `Mouse`). Search again or type `Q` to go back to the menu.
- **Colored messages** – green for success, red for errors, yellow for warnings, blue for information.

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Installation

```bash
git clone https://github.com/alakhmakova/ProductListManagementSystem.git
cd ProductListManagementSystem
dotnet run
```

## How to use

After start, the main menu is shown. Type the number of an option and press Enter.

```
**** PRODUCT LIST APPLICATION ****

1-Add a Product
2-Search a Product
3-List Products
0-Quit
Select Option 0-3:
```

### 1 – Add a product

```
Select Option 0-3: 1
---------------------------------------------------------------
To enter a new product - follow the steps | To quit - enter 'Q'
---------------------------------------------------------------
Enter Category: Electronics
Enter Product name: Mouse
Enter Price: -5
Price must be a number greater than 0 with at most 2 decimals
Enter Price: 299
The product was successfully added!

Enter Category: q
User cancelled the operation.
```

### 3 – List products

```
Select Option 0-3: 3
------------------------------------
Category      Product name     Price
------------------------------------
Food          Apple              120
Electronics   Mouse              299
------------------------------------
                          Total: 419
------------------------------------
```

### 2 – Search a product

Type part of a name or a category. The matching products are shown in a table. Type `Q` to go back to the menu.

```
Select Option 0-3: 2
---------------------------------------------------------------
Search by product name or category | To quit - enter 'Q'
---------------------------------------------------------------
Search: mou
------------------------------------
Category      Product name     Price
------------------------------------
Electronics   Mouse              299
------------------------------------
Search: xyz
Products not found.
Search: q
User cancelled the operation.
```

### 0 – Quit

Closes the application.

## Project structure

```
ProductListManagementSystem/
├── Program.cs                        ← entry point: starts ConsoleUI
├── Models/
│   └── Product.cs                    ← data for a single product + validation
├── Exceptions/
│   ├── InputClosedException.cs       ← input stream was closed (Ctrl+Z)
│   ├── ProductNotFoundException.cs   ← search found nothing
│   ├── UserCancelledException.cs     ← user typed Q
│   └── ValidationException.cs        ← invalid product data
├── Services/
│   └── ProductManager.cs             ← holds the product list and works with it
└── UI/
    ├── ConsoleUI.cs                  ← menu, table output, error handling
    └── InputHelper.cs                ← reads and validates input, colored messages
```

## Levels completed

- [x] Level 1 – Basic product list
- [x] Level 2 – Multiple classes & price summary
- [x] Level 3 – Error handling & dynamic product management
- [x] Level 4 – Search

## Credits

Created by Anastasiya Lakhmakova – GitHub: [@alakhmakova](https://github.com/alakhmakova)
