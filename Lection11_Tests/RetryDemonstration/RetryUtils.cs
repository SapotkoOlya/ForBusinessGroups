using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.RetryDemonstration
{
    public class RetryUtils
    {
        public delegate bool Operation();

        public static void Retry(int maxAttempt, Operation operation)
        {
            for (int attempt = 1; attempt <= maxAttempt; attempt++)
            {
                Console.WriteLine($"Попытка номер: {attempt}");
                if(operation())
                {
                    return; //механизм выхода из метода Retry
                }
            }

            throw new Exception($"За {maxAttempt} попыток нужный результат не был достигнут");
        }

        public static bool RetryWithReturnValue(int maxAttempt, Operation operation)
        {
            for (int attempt = 1; attempt <= maxAttempt; attempt++)
            {
                Console.WriteLine($"Попытка номер: {attempt}");
                if (operation())
                {
                    return true; //механизм выхода из метода Retry
                }
            }

            return false;
        }

        //ретрай со временем: ретраит пока не вернет true, либо пока не истечет время timeoutMs
        //время между повторными запусками метода - delayMs
        //если время вышло - бросаем исключение
        public static void RetryWithTimeout(Operation operation, int timeoutMs, int delayMs)
        {
            var start = DateTime.Now;

            for(int attempt = 1; ; attempt++)
            {
                Console.WriteLine($"{DateTime.Now} Попытка номер {attempt}");

                if(operation())
                {
                    return;
                }

                if((DateTime.Now - start).TotalMilliseconds >= timeoutMs)
                {
                    throw new Exception($"Операция не удалась за {timeoutMs} мс. Попыток было {attempt}");
                }

                Thread.Sleep(delayMs);
            }
        }

        // RetryUntilTrue с временем: ретраит, пока не вернёт true,
        // но не дольше timeoutMs, с паузой delayMs между попытками
        public static bool RetryUntilTrueWithTimeout(Operation op, int timeoutMs, int delayMs)
        {
            var start = DateTime.Now;

            for (int attempt = 1; ; attempt++)
            {
                Console.WriteLine($"---{DateTime.Now} Попытка #{attempt} ---");

                if (op())
                    return true;

                if ((DateTime.Now - start).TotalMilliseconds >= timeoutMs)
                    return false;

                Thread.Sleep(delayMs);
            }
        }
    }
}
