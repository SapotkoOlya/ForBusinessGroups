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
    public class AddressesRepository : IAddressesRepository
    {
        private readonly string connection;

        public AddressesRepository(string connection)
        {

            this.connection = connection;
        }

        public Task<IEnumerable<AddressDTO>> GetAllAddressesAsync()
        {
            using var db = new SqliteConnection(connection);

            const string sql = """
    SELECT Id, UserId, City
    FROM Addresses;
    """;

            return db.QueryAsync<AddressDTO>(sql);
        }

        public async Task<IEnumerable<(UserDTO user, AddressDTO address, OrderDTO order)>>
            GetAddressOrderCityMismatchesAsync()
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT 
            u.Id AS UserId, u.FirstName, u.LastName, u.Email,
            a.Id AS AddressId, a.UserId AS AddressUserId, a.City,
            o.Id AS OrderId, o.UserId AS OrderUserId, o.TotalPrice
        FROM Orders o
        JOIN Users u ON o.UserId = u.Id
        JOIN Addresses a ON a.UserId = u.Id
        WHERE a.City NOT IN (
            SELECT a2.City
            FROM Addresses a2
            WHERE a2.UserId = u.Id
        );
        """;

            var rows = await db.QueryAsync(sql);

            return rows.Select(r =>
            (
                new UserDTO { Id = (int)r.UserId, FirstName = (string)r.FirstName, LastName = (string)r.LastName, Email = (string)r.Email },
                new AddressDTO { Id = (int)r.AddressId, UserId = (int)r.AddressUserId, City = (string)r.City },
                new OrderDTO { Id = (int)r.OrderId, UserId = (int)r.OrderUserId, TotalPrice = (decimal)r.TotalPrice }
            ));
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAsync()
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<AddressDTO>("SELECT * FROM Addresses");
        }

        public async Task<IEnumerable<AddressDTO>> GetByUserAsync(int userId)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<AddressDTO>(
                "SELECT * FROM Addresses WHERE UserId = @userId", new { userId });
        }
    }
}
