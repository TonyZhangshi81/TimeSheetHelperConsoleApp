using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetHelperConsoleApp.Util
{
	/// <summary>
	/// 
	/// </summary>
	public static class Configuration
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T Get<T>(string key)
		{
			T result = default(T);

			string jsonfile = ConfigurationManager.AppSettings.Get(key);
			using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
			{
				result = JsonExtension.GetObjectByJson<T>(file.ReadToEnd());
			};

			return result;
		}
	}
}
