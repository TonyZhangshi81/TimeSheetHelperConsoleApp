using System;
using System.IO;
using System.Linq;
using System.Threading;
using TimeSheetHelperConsoleApp.Properties;
using TimeSheetHelperConsoleApp.Setting;
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
        private const string KEY_SETTING_CONFIG = "ParamArg";
        /// <summary>
        /// 
        /// </summary>
        private const string KEY_TEMPLATE = "Template";
        /// <summary>
        /// 
        /// </summary>
        private const string DATETIME_FORMAT = "yyyy/MM/dd HH:mm:ss";

        /// <summary>
        /// 
        /// </summary>
        static AttendanceManagement.Switch _inOut = AttendanceManagement.Switch.Default;

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
                // Default to the first input parameter (eg: TimeSheetHelperConsoleApp 0)
                args = new string[1] { "0" };
            }

            Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ConsoleMain_UnhandledException);

            var setting = Configuration.Get<SettingConfig>(KEY_SETTING_CONFIG);
            if (setting == null)
            {
                Console.WriteLine(Message.E004);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            var message = string.Empty;
            if (!CheckArguments(args, setting, ref message))
            {
                Console.WriteLine(message);
                Console.ReadKey();
                Environment.Exit(-1);
            }

            using (SpireXls xls = new SpireXls())
            {
                var isNewFile = false;
                var file = GetNewFileName();
                if (!File.Exists(file))
                {
                    isNewFile = true;
                    File.Copy(System.Configuration.ConfigurationManager.AppSettings.Get(KEY_TEMPLATE), file);
                }

                xls.Load(file);

                AttendanceManagement att = new AttendanceManagement(xls, setting, _inOut, isNewFile);
                att.Attendance();

                xls.Save();
            }

            Console.WriteLine(string.Format(_inOut == AttendanceManagement.Switch.Rest ? Message.I001 : Message.I002, DateTimeNow.DateTime.ToString(DATETIME_FORMAT)));
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        static private bool CheckArguments(string[] args, SettingConfig config, ref string message)
        {
            // switch check
            if (Enum.TryParse(args[0], out _inOut))
            {
                // switch the overflow check
                if (!Enum.IsDefined(typeof(AttendanceManagement.Switch), _inOut))
                {
                    message = Message.E002;
                    return false;
                }
            }
            else
            {
                message = Message.E002;
                return false;
            }

            // switch the default value check
            if (_inOut == AttendanceManagement.Switch.Default)
            {
                message = Message.E002;
                return false;
            }

            // redundant input parameters
            if (args.Count() > 2)
            {
                message = Message.E001;
                return false;
            }

            // take time to input parameters
            if (args.Count() == 2)
            {
                // set clock time
                if (!DateTimeNow.SetMistiming(args[1]))
                {
                    message = Message.E003;
                    return false;
                }
            }
            else
            {
                var threshold = (_inOut == AttendanceManagement.Switch.ClockIn) ? config.Hold.In : config.Hold.Out;
                // set clock time
                DateTimeNow.SetMistiming(threshold);
            }
            return true;
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
