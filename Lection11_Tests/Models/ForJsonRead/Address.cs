using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForJsonRead
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public Geo Geo { get; set; }
    }
}
