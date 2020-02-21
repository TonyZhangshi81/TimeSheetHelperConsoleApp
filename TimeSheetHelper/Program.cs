using System;
using System.IO;
using System.Linq;
using System.Threading;
using TimeSheetHelperConsoleApp.Ext;
using TimeSheetHelperConsoleApp.Util;
using TimeSheetHelperConsoleApp.WorkProcess;

namespace TimeSheetHelperConsoleApp
{
    /// <summary>
    /// 
    /// </summary>
	public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// [0] -> (0:ClockIn or 1:ClockOut)
        /// [1] -> (Time: 15:26:11)
        /// </param>
		static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                return;
            }

            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ConsoleMain_UnhandledException);

            var setting = Configuration.Get<SettingConfig>("ParamArg");

            var inOut = (AttendanceManagement.Switch)Enum.ToObject(typeof(AttendanceManagement.Switch), Convert.ToInt16(args[0]));

            if (args.Count() == 2)
            {
                DateTimeNow.SetMistiming(args[1]);
            }

            using (SpireXls xls = new SpireXls())
            {
                var file = GetNewFileName();
                if (!File.Exists(file)) {
                    File.Copy(System.Configuration.ConfigurationManager.AppSettings.Get("Template"), file);
                }

                xls.Load(file);

                AttendanceManagement att = new AttendanceManagement(xls, setting, inOut);
                att.Attendance();

                xls.Save();
            }

            Console.WriteLine(DateTimeNow.GetTime(inOut));
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static private string GetNewFileName()
        {
            return string.Format("{0}TimeSheet_zhangcg_{1}.xls",
                System.Configuration.ConfigurationManager.AppSettings.Get("WorkSheet"),
                DateTimeNow.GetBeginDayofWeek().ToString("yyyyMMdd").Substring(4, 4));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static private void ConsoleMain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
