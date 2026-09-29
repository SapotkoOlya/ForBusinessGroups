using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Hooks
{
    [SetUpFixture] //глобальные настройки перед стартом и концом тестов, выполняется 1 раз на прогон
    public class GlobalSetupAndTearDown
    {
        [OneTimeSetUp]
        public void GlobalOneTimeSetup()
        {
            Console.WriteLine("Выполяю один раз в начале.....");
        }

        [OneTimeTearDown]
        public void GlobalOneTimeTeardown()
        {
            Console.WriteLine("Выполяю один раз в конце.....");
        }
    }
}
