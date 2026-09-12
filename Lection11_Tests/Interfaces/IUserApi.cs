using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models;
using Refit;

namespace Lection11_Tests.Interfaces
{
    [Headers("x-api-key: free_user_3HMdkNLTV9xQwVXyEyCt3544HcE")]
    public interface IUserApi
    {
        // GET /users/2
        [Get("/users/{id}")]
        Task<UserResponse> GetUserAsync(int id);

        // POST /users
        [Post("/users")]
        Task<CreateUserResponse> CreateUserAsync([Body] CreateUserRequest request);

        // PUT /users/2
        [Put("/users/{id}")]
        Task<CreateUserResponse> UpdateUserAsync(int id, [Body] CreateUserRequest request);

        // DELETE /users/2
        [Delete("/users/{id}")]
        Task<ApiResponse<string>> DeleteUserAsync(int id);
    }
}
