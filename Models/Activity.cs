using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TimeCraft
{
    public abstract class Activity
    {
        public static bool IsStartDateCorrect(DateTime? startDate)
        {
            if (startDate == null) return false;
            return startDate.Value >= DateTime.Now;
        }

        public static bool IsTimeCorrect(string time)
        {
            if (time == null) return false;

            TimeSpan startTimeValue;
            if (TimeSpan.TryParse(time, out startTimeValue))
            {
                return DateTime.TryParseExact(time, "HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            }
            return false;
        }

        public static bool IsEndDateCorrect(DateTime? startDate, string startTime, DateTime? endDate, string endTime)
        {
            if (endDate == null || !IsTimeCorrect(startTime) || !IsTimeCorrect(endTime)) return false;

            if (endDate.Value == startDate.Value)
            {
                return TimeSpan.Parse(endTime) > TimeSpan.Parse(startTime);
            }
            else if (endDate.Value > startDate.Value)
            {
                return true;
            }
            return false;
        }
        public static bool IsTitleCorrect(string title)
        {
            return title.Length > 0;
        }
    }
}