namespace TimeSheetHelperConsoleApp.Setting
{
    /// <summary>
    /// 
    /// </summary>
    interface ISpireExt
    {
        /// <summary>
        /// Load an existing Excel file cannot be loaded by the password protected file
        /// </summary>
        /// <param name="filePathName">The file path name</param>
        void Load(string filePathName);

        /// <summary>
        /// Simple save an Excel file
        /// </summary>
        void Save();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="value"></param>
        void SetRangeText(string cellName, string value);
    }
}
