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
        /// Define a static variable to hold the instance of the class
        /// </summary>
        private static readonly DateTimeNow instance = new DateTimeNow();

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
            DateTime newDateTime = DateTime.Now;

            if (dateTime.Length == 14)
            {
                if (DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out newDateTime))
                {
                    SetDateTime(newDateTime);
                }
            }
            else if (dateTime.Length == 8)
            {
                if (DateTime.TryParseExact(dateTime, "yyyyMMdd", new CultureInfo("zh-CN", true), DateTimeStyles.None, out newDateTime))
                {
                    SetDateTime(newDateTime);
                }
            }
            else if (dateTime.Length == 6)
            {
                if (DateTime.TryParseExact(dateTime, "HHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out newDateTime))
                {
                    SetDateTime(newDateTime);
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
            return instance._dateTime.AddDays((dayIndex - 1) * -1);
        }

        /// <summary>
        /// Define public methods provide a global point of access, and you can also define the public property to provide a global point of access
        /// </summary>
        /// <returns></returns>
        public static DateTimeNow Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTime(AttendanceManagement.Switch inOut)
        {
            return GetTime(inOut, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static string GetTime(AttendanceManagement.Switch inOut, int threshold)
        {
            if (instance._inputDateTime)
            {
                return instance._dateTime.ToString("HH:mm:ss");
            }

            var inThreshold = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("InThreshold"));
            var outThreshold = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("OutThreshold"));

            return instance._dateTime.AddMinutes((inOut == AttendanceManagement.Switch.ClockIn) ? inThreshold : outThreshold).ToString("HH:mm:ss");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            return instance._dateTime;
        }

        static void SetDateTime(DateTime dateTime)
        {
            instance._inputDateTime = true;
            instance._dateTime = dateTime;
        }
    }
}
