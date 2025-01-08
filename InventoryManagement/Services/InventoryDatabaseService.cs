using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public class InventoryDatabaseService : IInventoryDatabaseService
    {
        private readonly string _connectionString;

        // Constructor accepts the connection string from App.config
        public InventoryDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new inventory item to the database
        public void AddInventoryItem(InventoryItem item)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                context.InventoryItems.Add(item);
                context.SaveChanges();
            }
        }

        // Retrieve all inventory items from the database
        public List<InventoryItem> GetInventoryItems()
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                return context.InventoryItems.ToList();
            }
        }

        // Update an existing inventory item
        public void UpdateInventoryItem(InventoryItem item)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var existingItem = context.InventoryItems.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Category = item.Category;
                    existingItem.LastUpdated = DateTime.Now;
                    existingItem.StockQuantity = item.StockQuantity;
                    context.SaveChanges();
                }
            }
        }

        // Delete an inventory item
        public void DeleteInventoryItem(InventoryItem item)
        {
            using (var context = new DatabaseContext(_connectionString))
            {
                var existingItem = context.InventoryItems.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    context.InventoryItems.Remove(existingItem);
                    context.SaveChanges();
                }
            }
        }
    }
}
