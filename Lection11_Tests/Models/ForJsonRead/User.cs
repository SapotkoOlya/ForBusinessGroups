using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lection11_Tests.Models.ForJsonRead
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Profile Profile { get; set; }
        public List<string> Roles { get; set; }
    }
}
