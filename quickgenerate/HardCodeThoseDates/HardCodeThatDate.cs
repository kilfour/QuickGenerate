using System;
using System.Collections.Generic;

namespace QuickGenerate.HardCodeThoseDates
{
    public static class HardCodeThatDate
    {
        public static DateTime January(this int day, int year) { return new DateTime(year, 1, day); }
        public static DateTime February(this int day, int year) { return new DateTime(year, 2, day); }
        public static DateTime March(this int day, int year) { return new DateTime(year, 3, day); }
        public static DateTime April(this int day, int year) { return new DateTime(year, 4, day); }
        public static DateTime May(this int day, int year) { return new DateTime(year, 5, day); }
        public static DateTime June(this int day, int year) { return new DateTime(year, 6, day); }
        public static DateTime July(this int day, int year) { return new DateTime(year, 7, day); }
        public static DateTime August(this int day, int year) { return new DateTime(year, 8, day); }
        public static DateTime September(this int day, int year) { return new DateTime(year, 9, day); }
        public static DateTime October(this int day, int year) { return new DateTime(year, 10, day); }
        public static DateTime November(this int day, int year) { return new DateTime(year, 11, day); }
        public static DateTime December(this int day, int year) { return new DateTime(year, 12, day); }

        public static string ToCode(this DateTime date)
        {
            return 
                string.Format(
                    "{0}.{1}({2})",
                    date.Day,
                    months[date.Month],
                    date.Year);
        }

        private static readonly Dictionary<int, string> months =
            new Dictionary<int, string>
            {
                {1, "January"},
                {2, "February"},
                {3, "March"},
                {4, "April"},
                {5, "May"},
                {6, "June"},
                {7, "July"},
                {8, "August"},
                {9, "September"},
                {10, "October"},
                {11, "November"},
                {12, "December"},
            };
    }
}