using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.DataProvider;
using Lection11_Tests.Components;

namespace Lection11_Tests.Tests
{
    [TestFixture]
    public class EmailValidatorTests
    {
        [TestCaseSource(typeof(EmailTestDataProvider), 
            nameof(EmailTestDataProvider.GetEmailCases))]
        public void EmailValidationTest(string email, bool result)
        {
            bool res = Validator.IsValid(email);
            Assert.That(res, Is.EqualTo(result),
                $"Емейл {email} не прошел валидацию, ожидалось {result}," +
                $" а на самом деле оказалось {res}");

        }
    }
}