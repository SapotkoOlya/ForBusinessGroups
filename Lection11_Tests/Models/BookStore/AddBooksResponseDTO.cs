using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.BookStore
{
    public class AddBooksResponseDTO
    {
        public string UserId { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
