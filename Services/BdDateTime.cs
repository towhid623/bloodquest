using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Helper
{
    public static class BdDateTime
    {
        public static DateTime Now()
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var offset = new DateTimeOffset(DateTime.UtcNow);
            return offset.AddHours(6).DateTime;
        }

        public static DateTime Today()
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var offset = new DateTimeOffset(DateTime.UtcNow);
            return offset.AddHours(6).DateTime.Date;
        }
    }
}