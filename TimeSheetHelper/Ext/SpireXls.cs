using Spire.Xls;
using System;

namespace TimeSheetHelperConsoleApp.Setting
{
    /// <summary>
    /// Excel read simple processing
    /// </summary>
    public class SpireXls : ISpireExt, IDisposable
    {
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
        string _filePathName { get; set; }

        /// <summary>
        /// The constructor
        /// </summary>
        public SpireXls()
        {
            _workBook = new Workbook();
        }

        #region Load the Excel file

        /// <summary>
        /// Load an existing Excel file cannot be loaded by the password protected file
        /// </summary>
        /// <param name="filePathName">The file name</param>
        public void Load(string filePathName)
        {
            _filePathName = filePathName;
            _workBook.LoadFromFile(_filePathName, ExcelVersion.Version97to2003);
        }

        #endregion

        #region Create and save to Excel

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="version"></param>
        public void Save()
        {
            // Will Excel file saved to the specified file, you can also specify the Excel version
            _workBook.SaveToFile(_filePathName, ExcelVersion.Version97to2003);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// 
        public void SetRangeText(string column, int row, string value)
        {
            this.SetRangeText(string.Format("{0}{1}", column, row), value);
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        public string GetRangeText(string cellName)
        {
            return Sheet.Range[cellName].Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public string GetRangeText(string column, int row)
        {
            return Sheet.Range[string.Format("{0}{1}", column, row)].Text;
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
        /// <param name="firstColumn"></param>
        /// <param name="lastColumn"></param>
        /// <param name="row"></param>
        /// <param name="colors"></param>
        public void SetRangeKnownColor(int firstColumn, int lastColumn, int row, ExcelColors colors)
        {
            _workSheet.Range[row, firstColumn, row, lastColumn].Style.KnownColor = colors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void DeleteRow(int row)
        {
            _workSheet.DeleteRow(row);
        }

        #endregion
    }
}
