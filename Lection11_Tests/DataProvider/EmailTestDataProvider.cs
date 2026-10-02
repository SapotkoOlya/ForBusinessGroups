using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.DataProvider
{
    //обязательно статический публичный класс
    public static class EmailTestDataProvider
    {
        private const string EmailDataFilePath = @"Resources\Email.csv";

        //именно так метод и оформляем
        public static IEnumerable<TestCaseData> GetEmailCases()
        {
            //получаем директорию в которой исполняется процесс запуска автотестов
            string baseDirectory = AppContext.BaseDirectory;
            //формируем полный путь к файлу с емейлами
            string fullPath = Path.Combine(baseDirectory, EmailDataFilePath);
            //читаем все строки из файла
            var lines = File.ReadAllLines(fullPath);

            for(int i=1; i<lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');
                string email = parts[0];
                bool result = bool.Parse(parts[1]);
                yield return new TestCaseData(email, result); //именно так оформляем возврат тестовых случаев
            }
        }
    }
}
