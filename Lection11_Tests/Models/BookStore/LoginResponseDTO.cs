using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.BookStore
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Expires { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
}
