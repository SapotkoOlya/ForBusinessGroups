using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
    }
}
