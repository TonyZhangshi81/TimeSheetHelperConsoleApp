namespace TimeSheetHelperConsoleApp.WorkProcess
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AttendanceManagement
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Switch : int
        {
            /// <summary>
            /// 
            /// </summary>
            ClockIn = 0,
            /// <summary>
            /// 
            /// </summary>
            ClockOut,
            /// <summary>
            /// 
            /// </summary>
            Rest,
            /// <summary>
            /// 
            /// </summary>
            Default
        }

        /// <summary>
        /// C
        /// </summary>
        private const string COL_NAME_CLOCKIN = "C";
        /// <summary>
        /// D
        /// </summary>
        private const string COL_NAME_CLOCKOUT = "D";



    }
}
