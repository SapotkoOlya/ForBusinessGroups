using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.BetterThanBad
{
    public class BetterTest
    {
        [Test]
        public void Test1()
        {
            var card = new CardPayment();
            var cash = new CashPayment();
            var wallet = new WalletPayment();

            // PaymentService
            var paymentService = new PaymentService(card);
            paymentService.Process(100);

            // OrderService
            var orderService = new OrderService(card);
            orderService.CompleteOrder(200);

            // SubscriptionService
            var subscriptionService = new SubscriptionService(cash);
            subscriptionService.Renew(300);
        }
    }
}
