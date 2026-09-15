using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.DI
{
    public class SubscriptionService
    {
        private readonly IPaymentMethod payment;

        public SubscriptionService(IPaymentMethod payment)
        {
            this.payment = payment;
        }

        public void Renew(decimal amount)
        {
            Console.WriteLine("Продление подписки...");
            payment.Pay(amount);
            Console.WriteLine("Подписка продлена.");
        }
    }
}
