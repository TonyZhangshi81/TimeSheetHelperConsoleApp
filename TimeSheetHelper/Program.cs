using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetHelperConsoleApp.Ext;
using TimeSheetHelperConsoleApp.Util;
using TimeSheetHelperConsoleApp.WorkProcess;
using static System.Net.Mime.MediaTypeNames;

namespace TimeSheetHelperConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			if (args.Count() == 0)
			{
				return;
			}

			if(args.Count() == 3)
			{
				DateTimeNow.SetMistiming(args[2]);
			}

			Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(ConsoleMain_UnhandledException);

			// 
			string fileName = args[0];
			// check on work attendance
			AttendanceManagement.Switch inOut = (AttendanceManagement.Switch)Enum.ToObject(typeof(AttendanceManagement.Switch), Convert.ToInt32(args[1]));

			using (SpireXls xls = new SpireXls())
			{
				xls.Load(fileName);

				AttendanceManagement att = new AttendanceManagement(xls, inOut);
				att.Attendance();

				xls.Save();
			}

			Console.WriteLine(DateTimeNow.GetTime());
			Console.ReadKey();
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
