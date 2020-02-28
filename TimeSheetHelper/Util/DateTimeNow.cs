using System;
using System.Globalization;
using TimeSheetHelperConsoleApp.WorkProcess;

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
        private bool _inputDateTime = false;

        /// <summary>
        /// Definition private constructor, so that the outside world cannot create the class instance
        /// </summary>
        private DateTimeNow()
        {
            _dateTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public static void SetMistiming(string dateTime)
        {
            DateTime sysDateTime = DateTime.Now;

            if (dateTime.Length == 14)
            {
                if (DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    SetDateTime(sysDateTime);
                }
            }
            else if (dateTime.Length == 8)
            {
                if (DateTime.TryParseExact(dateTime, "yyyyMMdd", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    SetDateTime(sysDateTime);
                }
            }
            else if (dateTime.Length == 6)
            {
                if (DateTime.TryParseExact(dateTime, "HHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out sysDateTime))
                {
                    SetDateTime(sysDateTime);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBeginDayofWeek()
        {
            var dayIndex = (int)DateTimeNow.GetDateTime().DayOfWeek;
            return Instance._dateTime.AddDays((dayIndex - 1) * -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inOut"></param>
        /// <returns></returns>
        public static string GetTime(AttendanceManagement.Switch inOut)
        {
            return GetTime(inOut, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inOut"></param>
        /// <param name="inThreshold"></param>
        /// <param name="outThreshold"></param>
        /// <returns></returns>
        public static string GetTime(AttendanceManagement.Switch inOut, int inThreshold, int outThreshold)
        {
            if (Instance._inputDateTime)
            {
                return Instance._dateTime.ToString("HH:mm:ss");
            }

            return Instance._dateTime.AddMinutes((inOut == AttendanceManagement.Switch.ClockIn) ? inThreshold : outThreshold).ToString("HH:mm:ss");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            return Instance._dateTime;
        }

        static void SetDateTime(DateTime dateTime)
        {
            Instance._inputDateTime = true;
            Instance._dateTime = dateTime;
        }
    }
}
