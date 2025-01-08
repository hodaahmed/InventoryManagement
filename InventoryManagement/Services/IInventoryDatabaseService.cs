using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services
{
    public interface IInventoryDatabaseService
    {
        void AddInventoryItem(InventoryItem item);
        void UpdateInventoryItem(InventoryItem item);
        void DeleteInventoryItem(InventoryItem item);
        List<InventoryItem> GetInventoryItems();
    }
}
