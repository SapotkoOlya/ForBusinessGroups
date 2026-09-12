using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductDTO>> GetProductsBoughtByUsersCountAsync(int minUsers);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        //old
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId);
        // сложные методы
        Task<IEnumerable<(ProductDTO Product, decimal TotalRevenue)>> GetTopSellingProductsAsync();
    }
}
