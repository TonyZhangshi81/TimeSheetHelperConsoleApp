using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetHelperConsoleApp.Ext;
using TimeSheetHelperConsoleApp.Util;

namespace TimeSheetHelperConsoleApp.WorkProcess
{
	/// <summary>
	/// check on work attendance
	/// </summary>
	public partial class AttendanceManagement
	{
		/// <summary>
		/// 
		/// </summary>
		private const string COL_NAME_CLOCKIN = "C";
		/// <summary>
		/// 
		/// </summary>
		private const string COL_NAME_CLOCKOUT = "D";

		/// <summary>
		/// 
		/// </summary>
		private SpireXls _xls { get; set; }
		/// <summary>
		/// 
		/// </summary>
		private Switch _inOut { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="workbook"></param>
		/// <param name="inOut"></param>
		public AttendanceManagement(SpireXls xls, Switch inOut)
		{
			_xls = xls;
			_inOut = inOut;

			CommonSetting();
		}

		/// <summary>
		/// 
		/// </summary>
		private void CommonSetting()
		{
			_xls.SetRangeText("PJ1", _xls.Config.Content.Id);
			_xls.SetRangeText("L4", _xls.Config.Content.Leader);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Attendance() => _xls.SetRangeText(string.Format("{0}{1}", GetHeadColName(), DayOfWeekToRowIndex()), DateTimeNow.GetTime());

		private int DayOfWeekToRowIndex()
		{
			switch (DateTimeNow.GetDateTime().DayOfWeek)
			{
				case DayOfWeek.Sunday:
					return 31;
				case DayOfWeek.Monday:
					return 13;
				case DayOfWeek.Tuesday:
					return 16;
				case DayOfWeek.Wednesday:
					return 19;
				case DayOfWeek.Thursday:
					return 22;
				case DayOfWeek.Friday:
					return 25;
				default:
					return 28;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private string GetHeadColName()
		{
			string colName = COL_NAME_CLOCKOUT;
			switch (_inOut)
			{
				case Switch.ClockIn:
					colName = COL_NAME_CLOCKIN;
					break;
				case Switch.ClockOut:
					colName = COL_NAME_CLOCKOUT;
					break;
			}
			return colName;
		}
	}
}
