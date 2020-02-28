using System;
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
        private void CommonSetting()
        {
            if (_isNewFile)
            {
                _xls.SetRangeText("PJ1", _config.Content.Id);
                _xls.SetRangeText("L4", _config.Content.Leader);

                _xls.SetRangeText("D5", _config.Content.JobNumber);
                _xls.SetRangeText("D6", _config.Content.Name);
                _xls.SetRangeText("D8", _config.Content.Seat);

                _xls.SetRangeText("D7", DateTimeNow.GetBeginDayofWeek().ToString("yyyy/MM/dd"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Attendance()
        {
            if (_inOut == Switch.Rest)
            {
                _xls.SetRangeText(string.Format("{0}{1}", COL_NAME_CLOCKIN, DayOfWeekToRowIndex()), string.Empty);
                _xls.SetRangeText(string.Format("{0}{1}", COL_NAME_CLOCKOUT, DayOfWeekToRowIndex()), string.Empty);
                return;
            }
            _xls.SetRangeText(string.Format("{0}{1}", GetHeadColName(), DayOfWeekToRowIndex()), DateTimeNow.GetTime(_inOut, _config.Hold.In, _config.Hold.Out));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
