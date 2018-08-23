using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetHelperConsoleApp.Ext
{
	/// <summary>
	/// 
	/// </summary>
	interface ISpireExt
	{
		/// <summary>
		/// Load an existing Excel file cannot be loaded by the password protected file
		/// </summary>
		/// <param name="fileName">The file name</param>
		void Load(string fileName);

		/// <summary>
		/// Simple save an Excel file
		/// </summary>
		void Save();

		/// <summary>
		/// Simple save an Excel file
		/// </summary>
		/// <param name="fileName">The file name</param>
		void Save(string fileName);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellName"></param>
		/// <param name="value"></param>
		void SetRangeText(string cellName, string value);
	}
}
