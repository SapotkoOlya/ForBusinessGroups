using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Hooks;

namespace Lection11_Tests.Tests.GroupTests
{
    [TestFixture]
    //[Parallelizable(ParallelScope.Fixtures)] //параллельно ходят фикстуры, а тесты внутри них идут последовательно
    //[Parallelizable(ParallelScope.Children)] //внутри фикстуры с тестами тесты идут параллельно, но сами фикстуры - последовательно друг за другом
    //[Parallelizable(ParallelScope.All)] //=[Parallelizable]
    //[Parallelizable(ParallelScope.Self)]//тест может идти параллельно с другими, но не с собственными параметризованными запусками
    public class Group1Tests : HooksForGroupTests1
    {
        [Test]
        [Parallelizable]
        [Description("")] //описание теста
        [Repeat(10)] //запустить тест 10 раз, один из 10 упал - красный весь тест
        public void Test11_ID12345()
        {
            Assert.IsTrue(true);
        }

        [Test]
        [Category("QA")]
        [Category("Fake")]
        [Timeout(30000)] //если тест не уложится в лимит, он будет отмечен красным (но он завершится!)
        public void Test12()
        {
            Assert.IsTrue(true);
        }

        [Test]
        [Ignore("reason")]
        [Order(1)]
        [Retry(2)] //перезапустить тест после падения
        public void Test13()
        {
            Assert.IsTrue(true);
        }
    }
}
