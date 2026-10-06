using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.RetryDemonstration
{
    public class AdditionalMethods
    {
        public static bool RandomSuccess()
        {
            var rnd = new Random();
            bool result = rnd.Next(0, 10) == 1;
            Console.WriteLine($"Результат: {result}");
            return result;
        }
    }
}
