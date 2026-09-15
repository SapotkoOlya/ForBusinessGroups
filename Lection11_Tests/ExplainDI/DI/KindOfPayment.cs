using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.ExplainDI.DI;

namespace Lection11_Tests.ExplainDI.DI
{
    public class CardPayment : IPaymentMethod
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата картой: {amount}");
        }
    }

    public class CashPayment : IPaymentMethod
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата наличкой: {amount}");
        }
    }

    public class WalletPayment : IPaymentMethod
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата электронным кошельком: {amount}");
        }
    }

}
