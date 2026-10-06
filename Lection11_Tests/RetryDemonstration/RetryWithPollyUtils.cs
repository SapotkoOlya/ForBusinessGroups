using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;

namespace Lection11_Tests.RetryDemonstration
{
    public class RetryWithPollyUtils
    {
        public delegate bool Operation();

        public static void RetryWithTimeout(Operation operation, int timeoutMs, int delayMs)
        {
            //правило повторных попыток
            var retryPolicy = Policy
                .HandleResult<bool>(r => !r)//если r равен false, то надо повторить, и настраиваем, как именно
                .WaitAndRetry(
                retryCount: int.MaxValue,//пока не кончится время, не важно сколько попыток будет нужно
                sleepDurationProvider: _ => TimeSpan.FromMilliseconds(delayMs),//ориентируюсь на время, а не число попыток
                //какие-то действия между попытками
                //outcome - результат последней попытки, что вернул метод
                //timespan - сколько полли ждет перед следующей попыткой
                //attempt - номер текущей попытки
                //context - можно передать свои данные
                onRetry: (outcome, timespan, attempt, context) =>
                {
                    Console.WriteLine($"{DateTime.Now} Попытка номер {attempt}");
                });

            //правила для времени
            var timeoutPolicy = Policy.Timeout(
                TimeSpan.FromMilliseconds(timeoutMs), TimeoutStrategy.Optimistic);

            timeoutPolicy.Wrap(retryPolicy).Execute(() =>
            {
                return operation();
            });
        }

        public static bool RetryUntilTrueWithTimeout(Operation op, int timeoutMs, int delayMs)
        {
            var retryPolicy = Policy
                .HandleResult<bool>(r => !r)
                .WaitAndRetry(
                    retryCount: int.MaxValue,
                    sleepDurationProvider: _ => TimeSpan.FromMilliseconds(delayMs),
                    onRetry: (outcome, timespan, attempt, context) =>
                    {
                        Console.WriteLine($"---{DateTime.Now} Попытка #{attempt} ---");
                    });

            var timeoutPolicy = Policy.Timeout(
                TimeSpan.FromMilliseconds(timeoutMs), TimeoutStrategy.Optimistic);

            try
            {
                return timeoutPolicy.Wrap(retryPolicy).Execute(() =>
                {
                    return op();
                });
            }
            catch
            {
                return false;
            }
        }
    }
}
