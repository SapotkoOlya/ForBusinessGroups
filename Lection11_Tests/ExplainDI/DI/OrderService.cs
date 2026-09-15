using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.DI
{
    public class OrderService
    {
        private readonly IPaymentMethod payment;

        public OrderService(IPaymentMethod payment)
        {
            this.payment = payment;
        }

        public void CompleteOrder(decimal amount)
        {
            Console.WriteLine("Создание заказа...");
            payment.Pay(amount);
            Console.WriteLine("Заказ завершён.");
        }
    }

}
