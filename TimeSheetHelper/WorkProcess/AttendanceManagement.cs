using System;
using System.Linq;
using TimeSheetHelperConsoleApp.Setting;
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
        private SpireXls _xls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private Switch _inOut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private SettingConfig _config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool _isNewFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xls"></param>
        /// <param name="config"></param>
        /// <param name="inOut"></param>
        /// <param name="isNewFile"></param>
        public AttendanceManagement(SpireXls xls, SettingConfig config, Switch inOut, bool isNewFile = false)
        {
            _xls = xls;
            _config = config;
            _inOut = inOut;
            _isNewFile = isNewFile;

            CommonSetting();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsHoliday(DateTime date)
        {
            return _config.Holiday.Days.Any(d => d.Equals(string.Format("{0}{1}", date.Month, date.Day)));
        }

        /// <summary>
        /// 
        /// </summary>
        private void CommonSetting()
        {
            if (_isNewFile)
            {
                var index = 3;
				string dayValue;
				do
				{
					var year = DateTimeNow.DateTime.Year;
					DateTime.TryParse(string.Format("{0}/{1}", year, _config.Timespan.Begin), out DateTime date);
					date = date.AddDays(index - 3);

					dayValue = string.Format("{0}/{1}", date.Month, date.Day);
					_xls.SetRangeText("C", index, dayValue);

					if (DayOfWeek.Saturday == date.DayOfWeek || DayOfWeek.Sunday == date.DayOfWeek || this.IsHoliday(date))
					{
						_xls.SetRangeKnownColor(2, 8, index, Spire.Xls.ExcelColors.Gray50Percent);
					}

					index++;
				} while (!_config.Timespan.End.Equals(dayValue));
			}
        }

        /// <summary>
        /// 
        /// </summary>
        public void Attendance()
        {
            /*
            if (_inOut == Switch.Rest)
            {
                _xls.SetRangeText(string.Format("{0}{1}", COL_NAME_CLOCKIN, DayOfWeekToRowIndex()), string.Empty);
                _xls.SetRangeText(string.Format("{0}{1}", COL_NAME_CLOCKOUT, DayOfWeekToRowIndex()), string.Empty);
                return;
            }
            _xls.SetRangeText(string.Format("{0}{1}", GetHeadColName(), DayOfWeekToRowIndex()), DateTimeNow.Time);
            */

            var rowIndex = this.GetIndex();
            if (rowIndex == 0)
            {
                return;
            }

            if (_inOut == Switch.ClockIn)
            {
                _xls.SetRangeText(string.Format("B{0}", rowIndex), DateTimeNow.Time);
                _xls.SetRangeText(string.Format("C{0}", rowIndex), DateTimeNow.DateTime.ToString("M/d"));
            }

            if (_inOut == Switch.ClockOut)
            {
                _xls.SetRangeText(string.Format("F{0}", rowIndex), DateTimeNow.Time);

                var begin = _xls.GetRangeText(string.Format("B{0}", rowIndex));
                var overtime = Convert.ToDateTime(string.Format("{0} {1}", DateTimeNow.DateTime.ToString("yyyy/MM/dd"), begin));

                if (DateTimeNow.DateTime.AddHours(-9).ToString("HH:mm:ss").CompareTo(begin) > 0)
                {
                    overtime = overtime.AddHours(9);

                    TimeSpan ts = DateTimeNow.DateTime.Subtract(overtime);
                    if (ts.TotalHours >= 0.5)
                    {
                        _xls.SetRangeText(string.Format("E{0}", rowIndex), overtime.ToString("HH:mm:ss"));
                        _xls.SetRangeText(string.Format("D{0}", rowIndex), Math.Round(ts.TotalHours, 1).ToString());
                    }
                }
                else
                {
                    TimeSpan ts = DateTimeNow.DateTime.Subtract(overtime);
                    if (Convert.ToDateTime(begin).Hour < 12)
                    {
                        _xls.SetRangeText(string.Format("H{0}", rowIndex), (8 - Math.Round(ts.TotalHours - 1, 1)).ToString());
                    }
                    else
                    {
                        _xls.SetRangeText(string.Format("H{0}", rowIndex), (8 - Math.Round(ts.TotalHours, 1)).ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetIndex()
        {
            var day = DateTimeNow.DateTime.ToString("M/d");
            for (var i = 3; i <= 33; i++)
            {
                var value = _xls.GetRangeText(string.Format("C{0}", i));
                if (day.Equals(value))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int DayOfWeekToRowIndex()
        {
            switch (DateTimeNow.DateTime.DayOfWeek)
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
