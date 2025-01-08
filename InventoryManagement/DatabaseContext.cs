using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class DatabaseContext : DbContext
    {
        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DatabaseContext(string connectionString) : base("name=InventoryDb")
        {
        }
    }
}
