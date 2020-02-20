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
        /// Define a static variable to hold the instance of the class
        /// </summary>
        private static readonly DateTimeNow instance = new DateTimeNow();

        /// <summary>
        /// Define a marks to ensure thread synchronization defines a thread synchronization
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// Definition private constructor, so that the outside world cannot create the class instance
        /// </summary>
        private DateTimeNow()
        {
            _dateTime = DateTime.Now.AddMinutes(Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings.Get("Threshold")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public static void SetMistiming(string dateTime)
        {
            DateTime newDateTime;

            if (dateTime.Length == 14)
            {
                if (DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out newDateTime))
                {
                    SetDateTime(newDateTime);
                }
            }
            else if(dateTime.Length == 6)
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
        public static string GetTime()
        {
            return instance._dateTime.ToString("HH:mm:ss");
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
            instance._dateTime = dateTime;
        }
    }
}
