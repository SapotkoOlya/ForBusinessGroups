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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string connection;

        public CategoryRepository(string connection)
        {

            this.connection = connection;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryAsync<CategoryDTO>("SELECT * FROM Categories");
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            using var db = new SqliteConnection(connection);
            return await db.QueryFirstOrDefaultAsync<CategoryDTO>(
                "SELECT * FROM Categories WHERE Id = @id", new { id });
        }
    }
}
