using System;
using System.Configuration;

namespace TimeSheetHelperConsoleApp.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key) where T : class
        {
            T result = default(T);
            try
            {
                string jsonfile = ConfigurationManager.AppSettings.Get(key);
                using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
                {
                    result = JsonExtension.GetObjectByJson<T>(file.ReadToEnd());
                };

                return result;
            }
            catch
            {
                return result;
            }
        }
    }
}
