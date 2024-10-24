﻿using System;
using System.Globalization;

namespace TimeCraft
{
    public abstract class Activity
    {
        public static bool IsStartDateCorrect(DateTime? startDate)
        {
            if (startDate == null) return false;
           bool t = startDate.Value >= DateTime.Now;
            return t;
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