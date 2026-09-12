using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForPetStore2
{
    public class CreateUserRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
    }

    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserResponseDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public string Expires { get; set; }
        public string Status { get; set; }
    }

    public class UserInfoDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class DeleteUserResponseDTO
    {
        public string Message { get; set; }
    }


}
