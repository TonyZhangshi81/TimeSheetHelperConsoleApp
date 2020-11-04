using Spire.Xls;

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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        void SetRangeText(string column, int row, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="value"></param>
        void SetRangeText(string cellName, string value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        string GetRangeText(string cellName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        string GetRangeText(string column, int row);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstColumn"></param>
        /// <param name="lastColumn"></param>
        /// <param name="row"></param>
        /// <param name="colors"></param>
        /// <returns></returns>
        void SetRangeKnownColor(int firstColumn, int lastColumn, int row, ExcelColors colors);

        void DeleteRow(int row);
    }
}
