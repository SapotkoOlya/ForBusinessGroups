using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Models.ForDapper
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
