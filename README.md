📦 Inventory Management System
======================================================================================================================================================================
🖥️ Overview
=======================================
The Inventory Management System is a desktop application built with WPF (Windows Presentation Foundation) for managing inventory items. It allows users to perform CRUD operations (Create, Read, Update, Delete) on inventory data, including filtering and searching inventory based on stock levels and item names. The application uses a SQL database for persistent storage and follows the MVVM (Model-View-ViewModel) design pattern.
================================================================================================
🚀 Features
==================================================
🔹 Add New Item: Add new inventory items with details like name, category, and stock quantity.<br/>
🔹 Edit Item: Edit details of existing items.<br/>
🔹 Delete Item: Remove inventory items.<br/>
🔹 Search Items: Search for items by name.<br/>
🔹 Stock Filtering: Filter inventory based on stock levels (e.g., Low Stock or In Stock).<br/>
🔹 Data Persistence: All changes are saved to a database.<br/>
<br/><br/>
⚙️ Technologies Used
================================================
💻 WPF (Windows Presentation Foundation) – User interface framework.<br/>
💡 MVVM (Model-View-ViewModel) – Design pattern for separation of concerns.<br/>
🐱 C# – Programming language used for the application logic.<br/>
🔗 Entity Framework – ORM used for database operations.<br/>
💾 SQL Server – Database for storing inventory data.<br/>
🔌 Dependency Injection (DI) – Used for service management.<br/>
<br/><br/>
🛠️ Installation Guide
===============================================
🛑 Prerequisites
===================================
Before you begin, ensure that the following tools are installed:<br/>

🔹 .NET Framework <br/>
🔹Visual Studio 2019+.<br/>
🔹SQL Server or a local SQL Server instance (use SQL Server Management Studio if needed).<br/>
<br/><br/>
📥 Clone the Repository
=================================
Clone this repository to your local machine using the following command:

bash
Copy code
git clone https://github.com/yourusername/InventoryManagement.git
<br/><br/>
⚙️ Set up the Database
==============================
🔹Create Database: Open SQL Server Management Studio (SSMS) and create a new database (e.g., InventoryDb).<br/>

🔹Update Connection String: In the project, update the connection string in App.config or appsettings.json:<br/>

xml
Copy code
<connectionStrings>
    <add name="InventoryDb" connectionString="Server=localhost;Database=InventoryDb;Integrated Security=True;" providerName="System.Data.SqlClient" />
</connectionStrings>
Run Database Script: Execute the necessary SQL scripts to create tables (if not done automatically).<br/>
<br/><br/>
🚀 Running the Application
=====================================
🔹Open the solution (InventoryManagement.sln) in Visual Studio.<br/>
🔹Press F5 or click Start to run the application.<br/>
🔹The main window will open with options to Add, Edit, Delete, Search, and Filter inventory items.<br/>
🔹Interact with the UI to manage your inventory.<br/>
  <br/><br/>
🧑‍💻 How It Works
=======================================
Architecture: MVVM Pattern <br/>
This application follows the MVVM (Model-View-ViewModel) design pattern for clear separation of concerns:
<br/>
🔹Model: Represents the data and business logic (e.g., InventoryItem, InventoryDatabaseService).<br/>
🔹View: The UI components (e.g., MainWindow.xaml, AddItemWindow.xaml).<br/>
🔹ViewModel: Acts as a mediator between the View and Model (e.g., InventoryViewModel).<br/>
🔹ViewModel and Commands<br/>
   🔹AddItemCommand: Executes the command to add a new item to the inventory.<br/>
   🔹EditItemCommand: Allows for editing existing inventory items.<br/>
   🔹DeleteItemCommand: Handles the deletion of inventory items.<br/>
   🔹SearchCommand: Enables the search functionality for inventory items.<br/>
   🔹StockFilter: Filters the inventory based on stock levels.<br/>
<br/><br/>
🛠️ Dependency Injection
==================================================================================
This application uses Dependency Injection (DI) to decouple components and manage dependencies. Services like IInventoryDatabaseService are injected into the ViewModel, making the code more modular and testable.
<br/><br/>
