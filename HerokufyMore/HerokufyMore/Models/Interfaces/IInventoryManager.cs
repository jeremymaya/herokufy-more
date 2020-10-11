using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerokufyMore.Models.Interfaces
{
    public interface IInventoryManager
    {
        Task<List<Product>> GetProducts();
    }
}
