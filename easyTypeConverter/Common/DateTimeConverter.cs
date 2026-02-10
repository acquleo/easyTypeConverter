using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyTypeConverter.Common
{
    public class DateTimeConverter
    {
        public DateTime Convert(DateTime dt, DateTimeKind target, TimeZoneInfo? timeZone = null)
        {
            var tz = timeZone == null ? TimeZoneInfo.Local : timeZone;
            switch (target)
            {
                case DateTimeKind.Utc:
                    if (dt.Kind == DateTimeKind.Local || dt.Kind == DateTimeKind.Unspecified)
                        return TimeZoneInfo.ConvertTimeToUtc(dt, tz);
                    break;
                case DateTimeKind.Local:
                    if (dt.Kind == DateTimeKind.Utc || dt.Kind == DateTimeKind.Unspecified)
                        return TimeZoneInfo.ConvertTimeFromUtc(dt, tz);
                    break;

            }
            return dt;
        }

        public DateTimeOffset Convert(DateTimeOffset dto, DateTimeKind target, TimeZoneInfo? timeZone = null)
        {

            if(target== DateTimeKind.Unspecified)
                return dto;            

            var tz = timeZone != null ? timeZone : target== DateTimeKind.Utc? TimeZoneInfo.Utc: TimeZoneInfo.Local;
            switch (target)
            {
                case DateTimeKind.Utc:
                    return TimeZoneInfo.ConvertTime(dto, tz);
                case DateTimeKind.Local:
                    return TimeZoneInfo.ConvertTime(dto, tz);

            }
            return dto;
        }
    }
}
