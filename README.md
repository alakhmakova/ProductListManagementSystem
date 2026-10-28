# Product List Management System

A C# console application for managing a product list: add products, view them sorted by price with a total, and search with highlighted results.

## Features

- Add products (category, name, price) until you type `q`
- Show all products in a table, sorted from lowest to highest price
- Display the total price at the bottom
- Validate input: empty names, non-numeric and negative prices are rejected
- Continue adding products after the list is shown
- Search products by name or category, with matches highlighted in color

## How to run

Requires the [.NET SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

## Project structure

```
ProductListManagement/
├── Program.cs              ← entry point: creates the objects and starts the app
├── Models/
│   └── Product.cs          ← data for a single product
├── Exceptions/
│       ├── ProductNotFoundException.cs
│       ├── ValidationException.cs          
├── Services/
│   └── ProductManager.cs   ← holds the product list and works with it
└── UI/
    ├── ConsoleUI.cs        ← menu, table output, colors
    └── InputHelper.cs      ← reads and validates user input
```

## Levels completed

- [ ] Level 1 – Basic product list
- [ ] Level 2 – Multiple classes & price summary
- [ ] Level 3 – Error handling & dynamic product management
- [ ] Level 4 – Search & highlight

