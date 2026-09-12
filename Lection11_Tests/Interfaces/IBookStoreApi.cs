using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.BookStore;
using Refit;

namespace Lection11_Tests.Interfaces
{
    public interface IBookStoreApi
    {
        // Создать пользователя
        [Post("/Account/v1/User")]
        Task<CreateUserResponseDTO> CreateUserAsync([Body] CreateUserDTO user);

        // Логин
        //[Post("/Account/v1/Login")]
        //Task<string> LoginAsync([Body] LoginRequestDTO user);

        [Post("/Account/v1/GenerateToken")]
        Task<TokenResponseDTO> GenerateTokenAsync([Body] LoginRequestDTO user);

        // Получить пользователя
        [Get("/Account/v1/User/{userId}")]
        Task<UserInfoDTO> GetUserAsync(string userId, [Header("Authorization")] string token);

        // Добавить книги пользователю
        [Post("/BookStore/v1/Books")]
        Task<AddBooksResponseDTO> AddBooksAsync([Body] AddBooksRequestDTO request,
                                                [Header("Authorization")] string token);

        // Удалить книгу
        [Delete("/BookStore/v1/Book")]
        Task<DeleteBookResponseDTO> DeleteBookAsync([Query] string UserId,
                                                    [Query] string ISBN,
                                                    [Header("Authorization")] string token);

        [Get("/BookStore/v1/Books")]
        Task<BooksListDTO> GetBooksAsync();

        [Get("/BookStore/v1/Book")]
        Task<BookDTO> GetBookAsync([Query] string ISBN);

        [Post("/Account/v1/Login")]
        Task<LoginUserResponseDTO> LoginUserAsync([Body] LoginRequestDTO user);

    }
}
