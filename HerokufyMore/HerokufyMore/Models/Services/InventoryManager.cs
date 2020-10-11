using HerokufyMore.Data;
using HerokufyMore.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokufyMore.Models.Services
{
    public class InventoryManager : IInventoryManager
    {
        private readonly StoreDbContext _context;

        public InventoryManager(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }
    }
}
