using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Interfaces.ForDapper;
using Lection11_Tests.Models.ForDapper;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Lection11_Tests.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string connection;

        public OrdersRepository(string connection)
        {

            this.connection = connection;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<OrderDTO>("SELECT * FROM Orders");
        }

        public async Task<OrderDTO?> GetByIdAsync(int id)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryFirstOrDefaultAsync<OrderDTO>(
                "SELECT * FROM Orders WHERE Id = @id", new { id });
        }

        public async Task<IEnumerable<OrderItemDTO>> GetItemsAsync(int orderId)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<OrderItemDTO>(
                "SELECT * FROM OrderItems WHERE OrderId = @orderId",
                new { orderId });
        }

        // Пересчёт суммы заказа
        public async Task<decimal> GetOrderTotalAsync(int orderId)
        {
            using var db = new SqliteConnection(connection);

            const string sql = @"
            SELECT SUM(oi.Quantity * oi.UnitPrice)
            FROM OrderItems oi
            WHERE oi.OrderId = @orderId";

            return await db.ExecuteScalarAsync<decimal>(sql, new { orderId });
        }

        // Заказ + его товары
        public async Task<IEnumerable<(OrderDTO Order, IEnumerable<OrderItemDTO> Items)>> GetOrdersWithItemsAsync()
        {
            using var db = new SqliteConnection(connection);

            var orders = await db.QueryAsync<OrderDTO>("SELECT * FROM Orders");

            var result = new List<(OrderDTO, IEnumerable<OrderItemDTO>)>();

            foreach (var order in orders)
            {
                var items = await GetItemsAsync(order.Id);
                result.Add((order, items));
            }

            return result;
        }

        public Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            using var db = new SqliteConnection(connection);
            const string sql = "SELECT Id, UserId, TotalPrice FROM Orders;";
            return db.QueryAsync<OrderDTO>(sql);
        }

        public Task<double> GetOrderItemsSumAsync(int orderId)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT SUM(Quantity * UnitPrice)
        FROM OrderItems
        WHERE OrderId = @OrderId;
        """;

            return db.ExecuteScalarAsync<double>(sql, new { OrderId = orderId });
        }

        public Task<IEnumerable<UserDTO>> GetUsersWithOrderTotalAboveAsync(double threshold)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT DISTINCT u.Id, u.FirstName, u.LastName, u.Email
        FROM Orders o
        JOIN Users u ON o.UserId = u.Id
        WHERE o.TotalPrice > @Threshold;
        """;

            return db.QueryAsync<UserDTO>(sql, new { Threshold = threshold });
        }
    }
}
