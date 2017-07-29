using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yuruisoft.RS.Web.Models
{
    #region 找回密码验证码Cache
    public class FindPSWcache
    {
        public FindPSWcache()
        {
            findPSWcache = new Dictionary<string, string> { };
        }
        public Dictionary<string, string> findPSWcache { get; set; }
    }
    public class SingleFindPSWcache
    {
        private volatile static FindPSWcache findPSWcache = null;
        private static readonly object lockHelper = new object();
        /// <summary>
        /// 获取FindPSWcache的单例
        /// </summary>
        /// <returns></returns>
        public static FindPSWcache GetFindPSWcache()
        {
            if (findPSWcache == null)
            {
                lock (lockHelper)
                {
                    findPSWcache = new FindPSWcache();
                }
            }
            return findPSWcache;
        }
    }
    #endregion
}