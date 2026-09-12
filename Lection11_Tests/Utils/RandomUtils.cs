using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Utils
{
    public static class RandomUtils
    {
        public static T GetRandomItem<T>(IList<T> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Список пустой или null");

            var rnd = new Random();
            int index = rnd.Next(items.Count);
            return items[index];
        }
    }
}
