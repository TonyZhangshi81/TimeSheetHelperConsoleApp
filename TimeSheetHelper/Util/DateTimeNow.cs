using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetHelperConsoleApp.Util
{
	/// <summary>
	/// 
	/// </summary>
	public class DateTimeNow
	{
		private readonly DateTime _dateTime;
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
			_dateTime = DateTime.Now;
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
	}
}
