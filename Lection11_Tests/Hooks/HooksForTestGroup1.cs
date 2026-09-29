using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Hooks
{
    public class HooksForTestGroup1 : GlobalSetupAndTearDown
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("Хуки для группы тестов 1 - выполнится 1 раз для всех тестов в одном классе");
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Хук для каждого теста из группы 1 (перед стартом каждого теста) в классе");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Хук для каждого теста из группы 1 (после выполнения каждого теста) в классе");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Хуки для группы тестов 1 - выполнится 1 раз для всех тестов в одном классе");
        }
    }
}
