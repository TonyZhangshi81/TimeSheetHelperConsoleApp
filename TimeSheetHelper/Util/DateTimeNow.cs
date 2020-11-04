using System;
using System.Globalization;

namespace TimeSheetHelperConsoleApp.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeNow
    {
        private DateTime _dateTime;

        /// <summary>
        /// Define public methods provide a global point of access, and you can also define the public property to provide a global point of access
        /// </summary>
        /// <returns></returns>
        public static DateTimeNow Instance { get; } = new DateTimeNow();

        /// <summary>
        /// Define a marks to ensure thread synchronization defines a thread synchronization
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Time => Instance._dateTime.ToString("HH:mm:ss");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime DateTime => Instance._dateTime;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime SysDateTime => DateTime.Now;

        /// <summary>
        /// Definition private constructor, so that the outside world cannot create the class instance
        /// </summary>
        private DateTimeNow()
        {
            _dateTime = DateTime.Now;
            if (DateTime.Now.ToString("HHmm").CompareTo("0830") <= 0)
            {
                DateTime.TryParseExact("083001", "HHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out _dateTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public static bool SetMistiming(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
            {
                return true;
            }

            DateTime sysDateTime;
            if (dateTime.Length == 14)
            {
                if (!DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    return false;
                }
            }
            else if (dateTime.Length == 12)
            {
                dateTime += "01";
                if (!DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    return false;
                }
            }
            else if (dateTime.Length == 8)
            {
                if (!DateTime.TryParseExact(dateTime, "yyyyMMdd", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    return false;
                }
            }
            else if (dateTime.Length == 6)
            {
                if (!DateTime.TryParseExact(dateTime, "HHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    return false;
                }
            }
            else if (dateTime.Length == 4)
            {
                dateTime += "01";
                if (!DateTime.TryParseExact(dateTime, "HHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            Instance._dateTime = sysDateTime;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool SetMistiming(int threshold)
        {
            Instance._dateTime = Instance._dateTime.AddMinutes(threshold);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBeginDayofWeek()
        {
            var dayIndex = (int)Instance._dateTime.DayOfWeek;
            return Instance._dateTime.AddDays((dayIndex - 1) * -1);
        }
    }
}
