using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lection11_Tests.ExplainDI.DI
{
    public static class DIContainer
    {
        public static ServiceProvider Build()
        {
            var services = new ServiceCollection();

            // выбираем реализацию
            services.AddTransient<IPaymentMethod, CardPayment>();

            // регистрируем сервисы
            services.AddTransient<PaymentService>();
            services.AddTransient<OrderService>();
            services.AddTransient<SubscriptionService>();

            return services.BuildServiceProvider();
        }
    }
}
