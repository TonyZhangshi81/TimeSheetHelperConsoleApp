using System.Runtime.Serialization;

namespace TimeSheetHelperConsoleApp.Ext
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
