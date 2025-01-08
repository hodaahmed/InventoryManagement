using InventoryManagement.Models;
using InventoryManagement.ViewModels;
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
using System.Windows.Shapes;

namespace InventoryManagement.Views
{
    /// <summary>
    /// Interaction logic for DeleteItemWindow.xaml
    /// </summary>
    public partial class DeleteItemWindow : Window
    {
       private InventoryViewModel viewModel;
        public DeleteItemWindow(InventoryItem item)
        {
            InitializeComponent();
             viewModel = new InventoryViewModel()
            {
                SelectedItem = item // Assign the item to be edited
            };

            // Set DataContext to the ViewModel
            DataContext = viewModel;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w is DeleteItemWindow)?.Close();
        }
    }
}
