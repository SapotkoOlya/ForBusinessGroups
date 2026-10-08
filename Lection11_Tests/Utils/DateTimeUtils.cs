using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection11_Tests.Utils
{
    public class DateTimeUtils
    {
        public static string GetFutureDateString(int day, string format)
        {
            return DateTime.Now.AddDays(day).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
