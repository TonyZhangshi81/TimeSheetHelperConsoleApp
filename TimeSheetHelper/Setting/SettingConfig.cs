using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TimeSheetHelperConsoleApp.Setting
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class SettingConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "threshold")]
        public Threshold Hold { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "content")]
        public BookContent Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "timespan")]
        public Timespan Timespan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "holiday")]
        public Holiday Holiday { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Holiday
    {
        [DataMember(Name = "days")]
        public List<string> Days { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Threshold
    {
        [DataMember(Name = "in")]
        public int In { get; set; }

        [DataMember(Name = "out")]
        public int Out { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BookContent
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "jobNumber")]
        public string JobNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "seat")]
        public string Seat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "leader")]
        public string Leader { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Timespan
    {
        [DataMember(Name = "begin")]
        public string Begin { get; set; }

        [DataMember(Name = "end")]
        public string End { get; set; }
    }
}
