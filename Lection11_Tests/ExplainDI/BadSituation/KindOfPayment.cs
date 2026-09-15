using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.BadSituation
{
    public class CardPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата картой: {amount}");
        }
    }

    public class CashPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата наличкой: {amount}");
        }
    }

    public class WalletPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата электронным кошельком: {amount}");
        }
    }
}
