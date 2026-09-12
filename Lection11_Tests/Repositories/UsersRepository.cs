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
    public class UsersRepository : IUsersRepository
    {
        private readonly string connection;

        public UsersRepository(string connection)
        {
            this.connection = connection;
        }

        public Task<IEnumerable<UserDTO>> GetUsersWhoBoughtCategoryAsync(string category)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT DISTINCT u.Id, u.FirstName, u.LastName, u.Email
        FROM Users u
        JOIN Orders o ON o.UserId = u.Id
        JOIN OrderItems oi ON o.Id = oi.OrderId
        JOIN Products p ON oi.ProductId = p.Id
        JOIN Categories c ON p.CategoryId = c.Id
        WHERE c.Name = @Category;
        """
            ;

            return db.QueryAsync<UserDTO>(sql, new { Category = category });
        }

        public Task<IEnumerable<UserDTO>> GetUsersWithReviewsInCategoryAsync(string category)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT DISTINCT u.Id, u.FirstName, u.LastName, u.Email
        FROM Users u
        JOIN Reviews r ON r.UserId = u.Id
        JOIN Products p ON r.ProductId = p.Id
        JOIN Categories c ON p.CategoryId = c.Id
        WHERE c.Name = @Category;
        """
            ;

            return db.QueryAsync<UserDTO>(sql, new { Category = category });
        }

        public Task<IEnumerable<UserDTO>> GetUsersWithOrdersCountAsync(int minOrders)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT u.Id, u.FirstName, u.LastName, u.Email
        FROM Users u
        JOIN Orders o ON o.UserId = u.Id
        GROUP BY u.Id
        HAVING COUNT(*) >= @MinOrders;
        """
            ;

            return db.QueryAsync<UserDTO>(sql, new { MinOrders = minOrders });
        }

        public Task<IEnumerable<UserDTO>> GetUsersNotInCitiesAsync(IEnumerable<int> userIds, IEnumerable<string> cities)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT DISTINCT u.Id, u.FirstName, u.LastName, u.Email
        FROM Users u
        JOIN Addresses a ON a.UserId = u.Id
        WHERE u.Id IN @UserIds
          AND a.City NOT IN @Cities;
        """;

            return db.QueryAsync<UserDTO>(sql, new { UserIds = userIds.ToArray(), Cities = cities.ToArray() });
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<UserDTO>("SELECT * FROM Users");
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryFirstOrDefaultAsync<UserDTO>(
                "SELECT * FROM Users WHERE Id = @id", new { id });
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesAsync(int userId)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<AddressDTO>(
                "SELECT * FROM Addresses WHERE UserId = @userId", new { userId });
        }

        // Все заказы пользователя
        public async Task<IEnumerable<OrderDTO>> GetUserOrdersAsync(int userId)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<OrderDTO>(
                "SELECT * FROM Orders WHERE UserId = @userId",
                new { userId });
        }

        // Все товары, которые пользователь покупал (JOIN Orders → OrderItems → Products)
        public async Task<IEnumerable<ProductDTO>> GetUserPurchasedProductsAsync(int userId)
        {
            using var db = new SqliteConnection(connection);

            const string sql = @"
            SELECT DISTINCT p.*
            FROM Products p
            JOIN OrderItems oi ON oi.ProductId = p.Id
            JOIN Orders o ON o.Id = oi.OrderId
            WHERE o.UserId = @userId";

            return await db.QueryAsync<ProductDTO>(sql, new { userId });
        }
    }
}
