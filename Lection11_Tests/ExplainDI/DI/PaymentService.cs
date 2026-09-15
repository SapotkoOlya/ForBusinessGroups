using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.DI
{
    public class PaymentService
    {
        private readonly IPaymentMethod payment;

        public PaymentService(IPaymentMethod payment)
        {
            this.payment = payment;
        }

        public void Process(decimal amount)
        {
            payment.Pay(amount);
        }
    }
}
