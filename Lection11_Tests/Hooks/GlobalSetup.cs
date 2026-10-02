using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    [SetUpFixture] //глобальные настройки запускаются 1 раз перед стартом всех автотестов
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Console.WriteLine("Выполняю 1 раз перед стартом всего.....");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Выполняю 1 раз после окончания всего.....");
        }
    }