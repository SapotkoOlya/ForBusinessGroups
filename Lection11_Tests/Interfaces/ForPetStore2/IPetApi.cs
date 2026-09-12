using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForPetStore2;
using Refit;

namespace Lection11_Tests.Interfaces.ForPetStore2
{
    public interface IPetApi
    {
        [Get("/pets/{id}")]
        Task<PetDTO> GetPetAsync(string id);

        [Get("/pets")]
        Task<AllPetsResponseDTO> GetAllPetsAsync();

        [Get("/pets")]
        Task<AllPetsResponseDTO> GetAllPetsByStatusAsync([Query] string status, [Query] int limit);

        /////////////////////////////////
        [Post("/users")]
        Task<CreateUserResponseDTO> CreatePetUserAsync([Body] CreateUserRequestDTO user);

        [Post("/users/login")]
        Task<LoginUserResponseDTO> LoginPetUserAsync([Body] LoginRequestDTO user);

        [Post("/users/token")]
        Task<TokenResponseDTO> GeneratePetTokenAsync([Body] LoginRequestDTO user);

        [Get("/users/{userId}")]
        Task<UserInfoDTO> GetPetUserAsync(string userId);

        [Delete("/users/{userId}")]
        Task<DeleteUserResponseDTO> DeletePetUserAsync(string userId);
    }
}
