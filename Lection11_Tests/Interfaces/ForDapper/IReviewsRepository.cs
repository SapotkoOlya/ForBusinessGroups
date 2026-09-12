using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForDapper;

namespace Lection11_Tests.Interfaces.ForDapper
{
    public interface IReviewsRepository
    {
        Task<IEnumerable<UserDTO>> GetUsersWithReviewsCountAsync(int minReviews);
        Task<IEnumerable<ProductDTO>> GetProductsWithReviewsAsync();
        Task<double> GetAverageRatingAsync(int productId);
        Task<double> CalculateAverageRatingAsync(int productId);
        Task<IEnumerable<ReviewDTO>> GetInvalidReviewsAsync();
        Task<IEnumerable<UserDTO>> GetUsersWithRatingAsync(int rating);

        ///////////
        Task<IEnumerable<ReviewDTO>> GetByProductAsync(int productId);
        Task<IEnumerable<ReviewDTO>> GetByUserAsync(int userId);
        // сложные методы

    }
}
