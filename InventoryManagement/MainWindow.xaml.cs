using InventoryManagement.Models;
using InventoryManagement.ViewModels;
using InventoryManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InventoryViewModel _inventoryViewModel;
        public MainWindow()
        {
            InitializeComponent();

            // Set the DataContext to the InventoryViewModel
            _inventoryViewModel = new InventoryViewModel();
            this.DataContext = new InventoryViewModel();
            this.StockFilterComboBox.ItemsSource = _inventoryViewModel.FilterOptions;
            // Load inventory items
            _inventoryViewModel.LoadInventoryItems();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void AddNewItemButton_Click(object sender, RoutedEventArgs e)
        {
            OpenAddItemWindow();
        }

    
        private void OpenAddItemWindow()
        {
            var addItemWindow = new AddItemWindow();
            // Subscribe to the Closed event
            addItemWindow.Closed += (s, args) =>
            {
                // Refresh grid after the delete window is closed
                if (DataContext is InventoryViewModel viewModel)
                {
                    viewModel.LoadInventoryItems();
                }
            };
            addItemWindow.ShowDialog();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as InventoryItem;

            if (item != null)
            {
                EditItemWindow editItemPage = new EditItemWindow(item);
                editItemPage.Show();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as InventoryItem;

            if (item != null)
            {
                DeleteItemWindow deleteItemPage = new DeleteItemWindow(item);
                // Subscribe to the Closed event
                deleteItemPage.Closed += (s, args) =>
                {
                    // Refresh grid after the delete window is closed
                    if (DataContext is InventoryViewModel viewModel)
                    {
                        viewModel.LoadInventoryItems();
                    }
                };
                deleteItemPage.Show();
            }

        }
    }
}
