using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.BadSituation
{
    public class OrderService
    {
        private readonly CardPayment card = new CardPayment();
        private readonly CashPayment cash = new CashPayment();
        private readonly WalletPayment wallet = new WalletPayment();

        public void CompleteOrder(string method, decimal amount)
        {
            Console.WriteLine("Создание заказа...");

            if (method == "card")
                card.Pay(amount);

            if (method == "cash")
                cash.Pay(amount);

            if (method == "wallet")
                wallet.Pay(amount);

            Console.WriteLine("Заказ завершён.");
        }
    }

}
