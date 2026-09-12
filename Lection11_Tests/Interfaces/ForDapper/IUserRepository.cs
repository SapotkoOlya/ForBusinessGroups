using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserDTO>> GetUsersWhoBoughtCategoryAsync(string category);
        Task<IEnumerable<UserDTO>> GetUsersWithReviewsInCategoryAsync(string category);
        Task<IEnumerable<UserDTO>> GetUsersWithOrdersCountAsync(int minOrders);
        Task<IEnumerable<UserDTO>> GetUsersNotInCitiesAsync(IEnumerable<int> userIds, IEnumerable<string> cities);

        //old
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AddressDTO>> GetAddressesAsync(int userId);
        // сложные методы
        Task<IEnumerable<OrderDTO>> GetUserOrdersAsync(int userId);
        Task<IEnumerable<ProductDTO>> GetUserPurchasedProductsAsync(int userId);
    }
}