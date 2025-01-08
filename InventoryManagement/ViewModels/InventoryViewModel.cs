using InventoryManagement.Models;
using InventoryManagement.Services;
using InventoryManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagement.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged 
    {
        private ObservableCollection<InventoryItem> _inventoryItems { get; set; }
        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SearchCommand { get; }
        //properties
        public string NewItemName { get; set; }
        public string NewItemCategory { get; set; }
        public int NewItemStockQuantity { get; set; }
        public InventoryItem SelectedItem { get; set; }

        private readonly IInventoryDatabaseService _databaseService;

        public event PropertyChangedEventHandler PropertyChanged;

        // Selected filter value for ComboBox (Low stock or In stock)
        // The collection that will be bound to the ComboBox
        private ObservableCollection<KeyValuePair<string, string>> _filterOptions;
        public ObservableCollection<KeyValuePair<string, string>> FilterOptions
        {
            get { return _filterOptions; }
            set
            {
                _filterOptions = value;
                OnPropertyChanged(nameof(FilterOptions));
            }
        }
        private string _stockFilter;
        public string StockFilter
        {
            get { return _stockFilter; }
            set
            {
                _stockFilter = value;
                OnPropertyChanged(nameof(StockFilter));
                FilterItemsByStock(); // Apply filter when the selected value changes
            }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                // Automatically trigger search when the text changes
                SearchItems();
            }
        }
        private ObservableCollection<InventoryItem> _filteredInventoryItems;
        public ObservableCollection<InventoryItem> FilteredInventoryItems
        {
            get => _filteredInventoryItems;
            set
            {
                _filteredInventoryItems = value;
                OnPropertyChanged(nameof(FilteredInventoryItems));
            }
        }

        public ObservableCollection<InventoryItem> InventoryItems
        {
            get { return _inventoryItems; }
            set
            {
                _inventoryItems = value;
                OnPropertyChanged(nameof(InventoryItems));
            }
        }

        public InventoryViewModel()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryDb"].ConnectionString;
            _databaseService = new InventoryDatabaseService(connectionString);
            AddItemCommand = new ItemCommand(AddItem, CanAddItem);
            EditItemCommand = new ItemCommand(EditItem, CanEditItem);
            DeleteItemCommand = new RelayCommand<InventoryItem>(DeleteItem, CanDeleteItem);
            _inventoryItems = new ObservableCollection<InventoryItem>(_databaseService.GetInventoryItems());
            _filteredInventoryItems = new ObservableCollection<InventoryItem>(_inventoryItems); // Initially show all items

            // Initialize filter options
            FilterOptions = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("All", "All"),
                new KeyValuePair<string, string>("Low Stock", "Low Stock"),
                new KeyValuePair<string, string>("In Stock", "In Stock")
            };
            LoadInventoryItems();
        }

        public InventoryViewModel(IInventoryDatabaseService databaseService)
        {
            _databaseService = databaseService;
            AddItemCommand = new ItemCommand(AddItem, CanAddItem);
            EditItemCommand = new ItemCommand(EditItem, CanEditItem);
            DeleteItemCommand = new RelayCommand<InventoryItem>(DeleteItem, CanDeleteItem);
            _inventoryItems = new ObservableCollection<InventoryItem>(_databaseService.GetInventoryItems());
            _filteredInventoryItems = new ObservableCollection<InventoryItem>(_inventoryItems); // Initially show all items

            // Initialize filter options
            FilterOptions = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("All", "All"),
                new KeyValuePair<string, string>("Low Stock", "Low Stock"),
                new KeyValuePair<string, string>("In Stock", "In Stock")
            };
            LoadInventoryItems();
        }

        public void AddItem(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewItemName) || NewItemStockQuantity <= 0 || string.IsNullOrEmpty(NewItemCategory))
            {
                return;
            }

            var newItem = new InventoryItem
            {
                Name = NewItemName,
                Category = NewItemCategory,
                StockQuantity = NewItemStockQuantity,
                LastUpdated = DateTime.Now
            };

            _databaseService.AddInventoryItem(newItem);
            InventoryItems.Add(newItem);
            NewItemName = string.Empty;
            NewItemStockQuantity = 0;
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w is AddItemWindow)?.Close();
        }

        public bool CanAddItem(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewItemName) && NewItemStockQuantity > 0 && ! string.IsNullOrEmpty(NewItemCategory);
        }
        // Method for editing an item
        public void EditItem(object parameter)
        {
            if (SelectedItem == null || string.IsNullOrWhiteSpace(SelectedItem.Name) || SelectedItem.StockQuantity <= 0 || string.IsNullOrWhiteSpace(SelectedItem.Category))
            {
                return;
            }

            // Update the item in the database
            _databaseService.UpdateInventoryItem(SelectedItem);

            // Update the ObservableCollection to reflect the changes
            var existingItem = _inventoryItems.FirstOrDefault(item => item.Id == SelectedItem.Id);
            if (existingItem != null)
            {
                existingItem.Name = SelectedItem.Name;
                existingItem.Category = SelectedItem.Category;
                existingItem.StockQuantity = SelectedItem.StockQuantity;
                existingItem.LastUpdated    = DateTime.Now;
            }
            LoadInventoryItems();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w is EditItemWindow)?.Close();

        }

        private bool CanEditItem(object parameter)
        {
            return SelectedItem != null && !string.IsNullOrWhiteSpace(SelectedItem.Name) && SelectedItem.StockQuantity > 0 && !string.IsNullOrWhiteSpace(SelectedItem.Category);
        }

        // Load all inventory items from the database
        public void LoadInventoryItems()
        {
            // Clear the current inventory items collection
            InventoryItems.Clear();

            // Retrieve all inventory items from the database
            var items = _databaseService.GetInventoryItems();

            // Add the retrieved items to the ObservableCollection
            foreach (var item in items)
            {
                InventoryItems.Add(item);
            }

            // Apply the current filter on the loaded inventory
            FilterItemsByStock();

        }

        // Delete the selected inventory item
        public void DeleteItem(object parameter)
        {
            var itemToDelete = SelectedItem as InventoryItem;
            if (itemToDelete == null)
            {
                return;
            }

            // Delete from the database
             _databaseService.DeleteInventoryItem(itemToDelete);

            // Remove from ObservableCollection (UI will update automatically)
            InventoryItems.Remove(itemToDelete);

            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w is DeleteItemWindow)?.Close();
        }

        // Check if the Delete button can be enabled (item must be selected)
        private bool CanDeleteItem(object parameter)
        {
            return SelectedItem != null;
        }

        // Method to search/filter items based on the search text
        public void SearchItems(object parameter = null)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                // If search text is empty, show all items
                FilteredInventoryItems = new ObservableCollection<InventoryItem>(_inventoryItems);
            }
            else
            {
                // Filter items based on search text (case-insensitive search for item names)
                var filteredItems = _inventoryItems
                    .Where(item => item.Name.Contains(SearchText))
                    .ToList();

                FilteredInventoryItems = new ObservableCollection<InventoryItem>(filteredItems);
            }
        }

        // Method to filter items based on the selected stock filter
        private void FilterItemsByStock()
        {
            if (StockFilter == "Low Stock")
            {
                FilteredInventoryItems = new ObservableCollection<InventoryItem>(
                    _inventoryItems.Where(item => item.StockQuantity < 10)
                );
            }
            else if (StockFilter == "In Stock")
            {
                FilteredInventoryItems = new ObservableCollection<InventoryItem>(
                    _inventoryItems.Where(item => item.StockQuantity >= 10)
                );
            }
            else
            {
                FilteredInventoryItems = new ObservableCollection<InventoryItem>(_inventoryItems);
            }
        }

        

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}