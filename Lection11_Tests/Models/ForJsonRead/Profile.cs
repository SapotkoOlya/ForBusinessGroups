using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForJsonRead
{
    public class Profile
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public List<string> Tags { get; set; }
    }
}
