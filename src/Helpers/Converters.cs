using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusForecast.Helpers
{
    static public class Converters
    {
        public static long ToJavascriptTimestamp(this DateTime input)
        {
            TimeSpan span = new TimeSpan(new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            DateTime time = input.Subtract(span);
            return (long)(time.Ticks / 10000);
        }
    }
}
