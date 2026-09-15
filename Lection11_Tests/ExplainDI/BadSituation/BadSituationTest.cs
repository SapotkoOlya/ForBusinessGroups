using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.BadSituation
{
    public class BadSituationTest
    {
        [Test]
        public void Test1()
        {
            PaymentService p = new PaymentService();
            p.Process("cash", 100);
            OrderService o = new OrderService();
            p.Process("wallet", 5);
        }
    }
}
