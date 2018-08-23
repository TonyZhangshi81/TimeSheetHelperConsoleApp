﻿using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TimeSheetHelperConsoleApp.Util;

namespace TimeSheetHelperConsoleApp.Ext
{
	/// <summary>
	/// Excel read simple processing
	/// </summary>
	public class SpireXls : ISpireExt, IDisposable
	{
		/// <summary>
		/// save the path
		/// </summary>
		public XlsConfig Config { get; private set; }
		/// <summary>
		/// 
		/// </summary>
		Workbook _workBook { get; set; }

		/// <summary>
		/// 
		/// </summary>
		private Worksheet _workSheet;
		/// <summary>
		/// 
		/// </summary>
		public Worksheet Sheet
		{
			get
			{
				if (_workSheet == null)
				{
					_workSheet = _workBook.ActiveSheet;
					if (_workSheet == null)
					{
						_workSheet = _workBook.Worksheets[0];
						_workBook.Worksheets[0].Activate();
					}
				}
				return _workSheet;
			}
			set
			{
				_workSheet = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		string _filePath { get; set; }

		/// <summary>
		/// The constructor
		/// </summary>
		public SpireXls()
		{
			_workBook = new Workbook();
			Config = Configuration.Get<XlsConfig>("XLS");
		}

		#region Load the Excel file

		/// <summary>
		/// Load an existing Excel file cannot be loaded by the password protected file
		/// </summary>
		/// <param name="fileName">The file name</param>
		public void Load(string fileName)
		{
			_filePath = string.Format("{0}{1}.xls", Config.Path, fileName);
			_workBook.LoadFromFile(_filePath, Config.Version);
		}

		#endregion

		#region Create and save to Excel
		/// <summary>
		/// Simple save an Excel file
		/// </summary>
		/// <param name="fileName">The file name</param>
		public void Save(string fileName)
		{
			// Will Excel file saved to the specified file, you can also specify the Excel version
			_workBook.SaveToFile(fileName, Config.Version);
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellName"></param>
		/// <param name="value"></param>
		/// <param name="style"></param>
		public void SetRangeText(string cellName, string value, CellStyle style)
		{
			Sheet.Range[cellName].Text = value;
			Sheet.Range[cellName].Style = style;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cellName"></param>
		/// <param name="value"></param>
		public void SetRangeText(string cellName, string value)
		{
			CellStyle style = Sheet.Range[cellName].Style;
			this.SetRangeText(cellName, value, style);
		}

		#region Release resources

		/// <summary>
		/// Release resources
		/// </summary>
		public void Dispose(Workbook workbook) => _workBook.Dispose();

		/// <summary>
		/// Release resources
		/// </summary>
		public void Dispose()
		{
			if (_workBook != null)
			{
				Dispose(_workBook);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Save()
		{
			Save(_filePath);
		}

		#endregion
	}
}