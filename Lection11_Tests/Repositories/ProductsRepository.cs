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
    public class ProductsRepository : IProductsRepository
    {
        private readonly string connection;

        public ProductsRepository(string connection)
        {

            this.connection = connection;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<ProductDTO>("SELECT * FROM Products");
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryFirstOrDefaultAsync<ProductDTO>(
                "SELECT * FROM Products WHERE Id = @id", new { id });
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<ProductDTO>(
                "SELECT * FROM Products WHERE CategoryId = @categoryId",
                new { categoryId });
        }

        // ТОП‑продаваемые товары
        public async Task<IEnumerable<(ProductDTO Product, decimal TotalRevenue)>> GetTopSellingProductsAsync()
        {
            using var db = new SqliteConnection(connection);

            const string sql = @"
        SELECT 
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.Stock,
            p.CategoryId,
            SUM(oi.Quantity * oi.UnitPrice) AS Revenue
        FROM Products p
        JOIN OrderItems oi ON oi.ProductId = p.Id
        GROUP BY p.Id
        ORDER BY Revenue DESC";

            var rows = await db.QueryAsync(sql);

            return rows.Select(r => (
                new ProductDTO
                {
                    Id = (int)r.Id,
                    Name = (string)r.Name,
                    Description = (string?)r.Description,
                    Price = (decimal)r.Price,
                    Stock = (int)r.Stock,
                    CategoryId = (int)r.CategoryId
                },
                (decimal)r.Revenue
            ));
        }

        public Task<IEnumerable<ProductDTO>> GetProductsBoughtByUsersCountAsync(int minUsers)
        {
            using var db = new SqliteConnection(connection);
            const string sql = """
        SELECT p.Id, p.Name, p.CategoryId
        FROM OrderItems oi
        JOIN Orders o ON oi.OrderId = o.Id
        JOIN Products p ON oi.ProductId = p.Id
        GROUP BY p.Id
        HAVING COUNT(DISTINCT o.UserId) >= @MinUsers;
        """;

            return db.QueryAsync<ProductDTO>(sql, new { MinUsers = minUsers });
        }

        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            using var db = new SqliteConnection(connection);
            const string sql = "SELECT Id, Name, CategoryId FROM Products;";
            return db.QueryAsync<ProductDTO>(sql);
        }
    }
}
