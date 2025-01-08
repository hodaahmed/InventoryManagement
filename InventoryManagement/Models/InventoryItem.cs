using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
