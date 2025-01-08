# InventoryManagement
Overview
The Inventory Management System is a WPF-based desktop application for managing inventory items. It allows users to perform CRUD operations (Create, Read, Update, Delete) on inventory items such as adding new items, editing existing items, deleting items, and searching/filtering through inventory data. The application uses a database to persist the inventory data and follows a MVVM (Model-View-ViewModel) design pattern.
==========================================================================================================
Features
Add New Item: Users can add new inventory items with details like Name, Category, and Stock Quantity.
Edit Item: Users can modify the details of existing items.
Delete Item: Users can delete inventory items from the system.
Search: Users can search inventory items based on name.
Filter by Stock: Users can filter items by stock levels, such as "Low Stock" or "In Stock".
Data Persistence: The application saves data to an SQL database.
=============================================================================================================
Technologies Used
WPF (Windows Presentation Foundation): User interface (UI) framework.
MVVM (Model-View-ViewModel): Design pattern for separation of concerns.
C#: Programming language.
Entity Framework: ORM used for database interactions.
SQL Server: Database system used to store inventory data.
Dependency Injection (DI): Used to inject services into the ViewModel.
============================================================================================================
Installation
Prerequisites
Before you begin, make sure you have the following installed:

.NET Framework 
Visual Studio 2019 or later
SQL Server (or use a local instance with SQL Server Management Studio)
Clone the Repository
Clone this repository to your local machine:

bash
Copy code
git clone https://github.com/yourusername/InventoryManagement.git
Open the solution (InventoryManagement.sln) in Visual Studio.
==========================================================
Set up the Database
Open SQL Server Management Studio (SSMS).

Create a new database for the application (e.g., InventoryDb).

Update the connection string in App.config or appsettings.json with the correct database credentials:

xml
Copy code
<connectionStrings>
    <add name="InventoryDb" connectionString="Server=localhost;Database=InventoryDb;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
Run the SQL script to create necessary tables (if not done automatically).
=============================================================================================================
Running the Application
Press F5 or click Start in Visual Studio to run the application.
The main window will open with options to Add, Edit, Delete, or Search inventory items.
Interact with the UI to manage your inventory.
How It Works
Architecture
The application is structured using the MVVM (Model-View-ViewModel) design pattern:

Model: Represents the data and business logic (e.g., InventoryItem, InventoryDatabaseService).
View: The UI (WPF XAML pages, such as MainWindow.xaml, AddItemWindow.xaml).
ViewModel: The middle layer that binds the View and Model (e.g., InventoryViewModel).
ViewModel and Commands
AddItemCommand: Binds to the "Add New Item" button to trigger the action of adding a new item to the inventory.
EditItemCommand: Allows for editing existing inventory items.
DeleteItemCommand: Handles the deletion of inventory items.
SearchCommand: Allows searching inventory items based on text input.
StockFilter: Filters inventory items by stock levels.
Dependency Injection
The application uses Dependency Injection (DI) to inject services such as IInventoryDatabaseService into the ViewModel. This ensures loose coupling between components and facilitates testing and maintenance.
=====================================================================================================================
Contributing
We welcome contributions to this project! If you find a bug or have a feature request, feel free to create an issue or submit a pull request.
=================================================================================================================
How to Contribute
Fork the repository.
Create a new branch for your changes (git checkout -b feature/new-feature).
Make your changes and commit them (git commit -am 'Add new feature').
Push to the branch (git push origin feature/new-feature).
Create a new pull request.
