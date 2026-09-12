using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<double> GetOrderItemsSumAsync(int orderId);
        Task<IEnumerable<UserDTO>> GetUsersWithOrderTotalAboveAsync(double threshold);

        //old
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO?> GetByIdAsync(int id);
        Task<IEnumerable<OrderItemDTO>> GetItemsAsync(int orderId);
        // сложные методы
        Task<decimal> GetOrderTotalAsync(int orderId);
        Task<IEnumerable<(OrderDTO Order, IEnumerable<OrderItemDTO> Items)>> GetOrdersWithItemsAsync();
    }
}
