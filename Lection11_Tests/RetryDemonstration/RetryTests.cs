using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lection11_Tests.RetryDemonstration
{
    [TestFixture]
    public class RetryTests
    {
        [Test]
        public void SimpleRetry()
        {
            RetryUtils.Operation op = AdditionalMethods.RandomSuccess;
            RetryUtils.Retry(5, op);
            // какие-то действия дальше
        }

        [Test]
        public void SimpleRetryWithReturnValue()
        {
            RetryUtils.Operation op = AdditionalMethods.RandomSuccess;
            var result = RetryUtils.RetryWithReturnValue(5, op);
            Assert.IsTrue(result, $"Ретрай вернул {result}, а ожидалось true");
            // какие-то действия дальше
        }

        [Test]
        public void SimpleRetryWithTimeout()
        {
            RetryUtils.Operation op = AdditionalMethods.RandomSuccess;
            RetryUtils.RetryWithTimeout(op, 40000, 5000);
            // какие-то действия дальше
        }

        [Test]
        public void SimpleRetryWithTimeoutPolly()
        {
            RetryWithPollyUtils.Operation op = AdditionalMethods.RandomSuccess;
            RetryWithPollyUtils.RetryWithTimeout(op, 60000, 2000);
            // какие-то действия дальше
        }
    }
}
