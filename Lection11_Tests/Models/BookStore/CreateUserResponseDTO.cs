using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.BookStore
{
    public class CreateUserResponseDTO
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
    }
}
