using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using WTS.BL.Models;
using WTS.BL.Utils;

namespace WTS.BL.Extensions
{
    public static class BlExtensions
    {
        public static RoleEnum GetUserRole(this IIdentity srcIdentity)
        {
            var identity = srcIdentity as ClaimsIdentity;
            var value = identity.Claims.Single(x => x.Type == ClaimTypes.Role).Value;

            return (RoleEnum) value.TryParse<long>();
        }

        public static TimeSpan? TryParseSpan(this string str)
        {
            DateTime result;
            if (DateTime.TryParseExact(str, "h:mm tt",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
                return result.TimeOfDay;
            return null;
        }

        public static DateTime? TryParseDate(this string str, string format = "dd/MM/yyyy")
        {
            DateTime startDate;
            if (DateTime.TryParseExact(str, format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, out startDate))
                return startDate;
            return null;
        }

        public static string ToStringTimeSpan(this TimeSpan? timeSpan)
        {
            if (timeSpan == null)
                return string.Empty;
            var time = DateTime.Today.Add(timeSpan.Value);
            var displayTime = time.ToString("h:mm tt");
            return displayTime;
        }
    }
}