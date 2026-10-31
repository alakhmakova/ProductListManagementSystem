# Product List Management System

A C# console application for managing a list of products. You can add products, see them in a table sorted by price with a total sum, search by product name or category, delete products by id, and keep your products between runs in a JSON file.

## Description

The app is built for a warehouse administrator who keeps track of products and their prices. It was written as a course assignment (Checkpoint 2) to practise object-oriented programming, LINQ, input validation, custom exceptions and working with files (JSON serialization) in C#.

The code is split into layers, so that each class has one job:

- **Models** – `Product` holds the data of one product and validates it in its constructor. `ProductData` is what is saved to the file: the products and the next free id.
- **Services** – `ProductManager` stores the product list and does the work on it: adding (with a new id), finding by id, deleting, sorting, searching and calculating the total. `ProductFileStorage` saves the list to a JSON file and loads it back. Neither of them writes to the console.
- **UI** – `ConsoleUI` shows the menu and the tables and catches errors; `InputHelper` reads and validates user input and prints colored messages.
- **Exceptions** – custom exceptions (`ValidationException`, `ProductsNotFoundException`, `UserCancelledException`, `InputClosedException`, `DataFileException`) carry error messages from the logic to the UI.

### Features

- **Add products** – enter category, name and price; keep adding until you type `Q` (works at any step).
- **Product IDs** – every new product automatically gets the next number: 1, 2, 3… An id is never given out again, even after the product is deleted or the app is restarted.
- **Input validation** – empty category or name is rejected; the price must be a number greater than 0 with at most 2 decimals. On invalid input the app asks again instead of crashing.
- **List products** – all products in a table with their ids, sorted from lowest to highest price, with the total at the bottom. Column widths adjust to the longest value.
- **Search** – by part of the product name or category, case-insensitive (`mouse` finds `Mouse`). Search again or type `Q` to go back to the menu.
- **Delete products** – enter a product id, check the product shown in a table and confirm with `y` or cancel with `n`. Type `Q` to go back to the menu.
- **Save & load** – the list is saved to `products.json` after every change (add or delete), and the list is loaded automatically when the app starts. If the file is missing (first run), the app starts with an empty list; if the file is corrupted or contains invalid data, the app shows a message and starts with an empty list instead of crashing.
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

The products are stored in `products.json` in the folder the app is started from (the project folder when you use `dotnet run`). The file is listed in `.gitignore`, so your data is not committed.

## How to use

After start, the app loads the saved products and shows the main menu. Type the number of an option and press Enter.

```
************************
PRODUCT LIST APPLICATION
************************
2 products loaded from file

1-Add a Product
2-Search a Product
3-List Products
4-Delete a Product
0-Quit
Select Option 0-4:
```

### 1 – Add a product

```
Select Option 0-4: 1
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
Select Option 0-4: 3
-----------------------------------------
Id   Category      Product name     Price
-----------------------------------------
1    Food          Apple              120
2    Electronics   Mouse              299
-----------------------------------------
                               Total: 419
-----------------------------------------
```

### 2 – Search a product

Type part of a name or a category. The matching products are shown in a table. Type `Q` to go back to the menu.

```
Select Option 0-4: 2
--------------------------------------------------------
Search by product name or category | To quit - enter 'Q'
--------------------------------------------------------
Search: mou
-----------------------------------------
Id   Category      Product name     Price
-----------------------------------------
2    Electronics   Mouse              299
-----------------------------------------
Search: xyz
Products not found.
Search: q
User cancelled the operation.
```

### 4 – Delete a product

Type the id of the product. The product is shown in a table, and you confirm with `y` or keep it with `n`. Type `Q` to go back to the menu.

```
Select Option 0-4: 4
-----------------------------------------------------------
Enter the id of the product to delete | To quit - enter 'Q'
-----------------------------------------------------------
Enter product id to delete: 7
Product with id 7 not found.
Enter product id to delete: 1
--------------------------------------
Id   Category   Product name     Price
--------------------------------------
1    Food       Apple              120
--------------------------------------
Delete this product? (y/n): y
The product was successfully deleted!

Enter product id to delete: q
User cancelled the operation.
```

### 0 – Quit

Closes the application.

## Project structure

```
ProductListManagementSystem/
├── Program.cs                        ← entry point: starts ConsoleUI
├── Models/
│   ├── Product.cs                    ← data for a single product + validation
│   └── ProductData.cs                ← what is saved: products + next id
├── Exceptions/
│   ├── InputClosedException.cs       ← input stream was closed (Ctrl+Z)
│   ├── ProductNotFoundException.cs   ← no product found (search or id)
│   ├── UserCancelledException.cs     ← user typed Q
│   ├── ValidationException.cs        ← invalid product data
│   └── DataFileException.cs          ← data file can't be read or written
├── Services/
│   ├── ProductManager.cs             ← holds the product list and works with it
│   └── ProductFileStorage.cs         ← saves and loads products (JSON)
└── UI/
    ├── ConsoleUI.cs                  ← menu, table output, error handling
    └── InputHelper.cs                ← reads and validates input, colored messages
```

## Levels completed

- [x] Level 1 – Basic product list
- [x] Level 2 – Multiple classes & price summary
- [x] Level 3 – Error handling & dynamic product management
- [x] Level 4 – Search
- [x] Level 5 (partly) – product ID system, delete products by id, save & load products from a JSON file

## Credits

Created by Anastasiya Lakhmakova – GitHub: [@alakhmakova](https://github.com/alakhmakova)
