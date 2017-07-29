using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yuruisoft.RS.Web.Models
{
    public class LogOnVcodeCache
    {
        public LogOnVcodeCache()
        {
            LogOnCache = new Dictionary<string, short> { };
        }
        public Dictionary<string, short> LogOnCache{ get;set;}
    }

    public class SingleLogOnVcodeCache
    {
        private volatile static LogOnVcodeCache logOnVcodeCache = null;
        private static readonly object lockHelper = new object();
        /// <summary>
        /// 获取LogOnVcodeCache的单例
        /// </summary>
        /// <returns></returns>
        public static LogOnVcodeCache GetLogOnVcodeCache()
        {
            if (logOnVcodeCache == null)
            {
                lock (lockHelper)
                {
                    logOnVcodeCache = new LogOnVcodeCache();
                }
            }
            return logOnVcodeCache;
        }
    }
}