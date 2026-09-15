using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lection11_Tests.ExplainDI.DI
{
    public class DITests
    {
        [Test]
        public void Test1()
        {
            var provider = DIContainer.Build();

            var payment = provider.GetRequiredService<PaymentService>();
            payment.Process(100);

            var order = provider.GetRequiredService<OrderService>();
            order.CompleteOrder(200);

            var sub = provider.GetRequiredService<SubscriptionService>();
            sub.Renew(300);
        }
    }
}
