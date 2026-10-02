using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Hooks
{
    public class HooksForGroupTests1 //это фактически BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("Выполняюсь 1 раз перед стартом всех тестов группы 1");
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Выполняюсь перед каждым тестом из группы 1");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Выполняюсь после каждого теста из группы 1");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Выполняюсь 1 раз после окончания прохождения всех тестов группы 1");
        }
    }
}
