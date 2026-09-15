using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.ExplainDI.DI
{
    public interface IPaymentMethod
    {
        void Pay(decimal amount);
    }
}
