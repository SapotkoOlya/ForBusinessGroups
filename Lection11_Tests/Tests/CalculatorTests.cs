using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Components;

namespace Lection11_Tests.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        
        [TestCase(1, 2, 3)]
        [TestCase(-2, -6, -8)]
        [TestCase(-5, 10, 5)]
        public void AddCalculatorTest(int a, int b, int result)
        {
            int res = Calculator.Add(a, b);
            Assert.That(res, Is.EqualTo(result),
                $"Expected {result} but was {res}, a = {a}, b = {b}");
        }
    }
}
