using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheetHelperConsoleApp.Ext
{
	/// <summary>
	/// 
	/// </summary>
	[DataContract]
	public class XlsConfig
	{
		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "path")]
		public string Path { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "version")]
		public ExcelVersion Version { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember(Name = "content")]
		public BookContent Content { get; set; }
	}

	[DataContract]
	public class BookContent
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "leader")]
		public string Leader { get; set; }
	}
}
