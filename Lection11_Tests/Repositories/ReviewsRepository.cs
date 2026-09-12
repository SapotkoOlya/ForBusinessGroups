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
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly string conn;

        public ReviewsRepository(string connection)
        {

            conn = connection;
        }

        public async Task<IEnumerable<ReviewDTO>> GetByProductAsync(int productId)
        {
            using var db = new SqliteConnection(conn);
            return await db.QueryAsync<ReviewDTO>(
                "SELECT * FROM Reviews WHERE ProductId = @productId",
                new { productId });
        }

        public async Task<IEnumerable<ReviewDTO>> GetByUserAsync(int userId)
        {
            using var db = new SqliteConnection(conn);
            return await db.QueryAsync<ReviewDTO>(
                "SELECT * FROM Reviews WHERE UserId = @userId",
                new { userId });
        }

        // Средний рейтинг товара
        public async Task<double> GetAverageRatingAsync(int productId)
        {
            using var db = new SqliteConnection(conn);

            const string sql = @"
            SELECT AVG(Rating)
            FROM Reviews
            WHERE ProductId = @productId";

            return await db.ExecuteScalarAsync<double>(sql, new { productId });
        }

        public Task<IEnumerable<UserDTO>> GetUsersWithReviewsCountAsync(int minReviews)
        {
            using var db = new SqliteConnection(conn);
            const string sql = """
        SELECT u.Id, u.FirstName, u.LastName, u.Email
        FROM Reviews r
        JOIN Users u ON r.UserId = u.Id
        GROUP BY u.Id
        HAVING COUNT(*) >= @MinReviews;
        """;

            return db.QueryAsync<UserDTO>(sql, new { MinReviews = minReviews });
        }

        public Task<IEnumerable<ProductDTO>> GetProductsWithReviewsAsync()
        {
            using var db = new SqliteConnection(conn);
            const string sql = """
        SELECT DISTINCT p.Id, p.Name, p.CategoryId
        FROM Reviews r
        JOIN Products p ON r.ProductId = p.Id;
        """;

            return db.QueryAsync<ProductDTO>(sql);
        }

        public Task<double> CalculateAverageRatingAsync(int productId)
        {
            using var db = new SqliteConnection(conn);
            const string sql = """
        SELECT SUM(Rating) * 1.0 / COUNT(*)
        FROM Reviews
        WHERE ProductId = @ProductId;
        """;

            return db.ExecuteScalarAsync<double>(sql, new { ProductId = productId });
        }

        public Task<IEnumerable<ReviewDTO>> GetInvalidReviewsAsync()
        {
            using var db = new SqliteConnection(conn);
            const string sql = """
        SELECT r.Id, r.UserId, r.ProductId, r.Rating
        FROM Reviews r
        WHERE NOT EXISTS (
            SELECT 1
            FROM Orders o
            JOIN OrderItems oi ON o.Id = oi.OrderId
            WHERE o.UserId = r.UserId
              AND oi.ProductId = r.ProductId
        );
        """;

            return db.QueryAsync<ReviewDTO>(sql);
        }

        public Task<IEnumerable<UserDTO>> GetUsersWithRatingAsync(int rating)
        {
            using var db = new SqliteConnection(conn);
            const string sql = """
        SELECT DISTINCT u.Id, u.FirstName, u.LastName, u.Email
        FROM Reviews r
        JOIN Users u ON r.UserId = u.Id
        WHERE r.Rating = @Rating;
        """;

            return db.QueryAsync<UserDTO>(sql, new { Rating = rating });
        }
    }
}
