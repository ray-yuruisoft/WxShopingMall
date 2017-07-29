using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.BLL;
using Yuruisoft.RS.DAL;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;

namespace RouteStatisticsCache
{
    /// <summary>
    /// 存放访问链接表的类，属性有一个键值对的缓存，存放链接表，并可以更新
    /// </summary>
    public class UrlCache
    {
        private Dictionary<string, string> UrlMap;
        private object _lock = new object();
        //缓存
        public Dictionary<string, string> URLMap
        {
            get
            {
                if (UrlMap == null)
                {
                    lock (_lock)
                    {
                        UrlMap = new Dictionary<string, string>();
                        //需要一个ID推广员 对应链接  的字典集合
                        IExtensionAgentsService EAS = new ExtensionAgentsService();//推广员表
                        IRouteStatisticsLinksService RSLS = new RouteStatisticsLinksService();//链接表
                        var EAS_results =  EAS.LoadEntities(c =>true);
                        var RSLS_results = RSLS.LoadEntities(c=>true);
                        var Union_results =  from c in EAS_results
                                             from o in RSLS_results
                                             where c.RouteStatisticsLinks_ID == o.ID
                                             select new{c.GUID,o.Url};
                         foreach (var item in Union_results)
                         {
                             UrlMap[item.GUID] = item.Url;
                         }
                    }
                }
                return UrlMap;
            }
            set
            {
                foreach (var item in value.Where(c => true))
                {

                            if (!UrlMap.ContainsKey(item.Key))
                            {
                                UrlMap.Add(item.Key, item.Value);
                            }
                    
                }
            }
        }
    }

    /// <summary>
    /// 存放访问次数表的类，属性有一个键值对的缓存，存放次数，并可以更新
    /// </summary>
    public class ScoreCache 
    {
        private Dictionary<string, long> _scoreMap;
        private object _lock = new object();
        public Dictionary<string, long> scoreMap
        {
            get
            {
                if (_scoreMap == null)
                {
                    lock (_lock)
                    {
                        _scoreMap = new Dictionary<string, long> { {"ModifyFlag",0} };//后期升级可以加入区分每个键值对的修改情况
                    }
                }
                return _scoreMap;
            }
            set
            {
                _scoreMap = value;
            }
        }
    }

    /// <summary>
    /// ScoreCache的操作类，只有一个方法，就是去让对应名称加一
    /// </summary>
    public class OperateScoreCache
    {
        private string GuidName { get; set; }
        public OperateScoreCache(string _GuidName)
        {
            GuidName = _GuidName;
        }
        public bool ScoreCacheAddOne()
        {
            ScoreCache scoreCache  = ExtendMethord.GetScore();
                if (scoreCache.scoreMap.ContainsKey(GuidName))
                {
                    scoreCache.scoreMap[GuidName] = scoreCache.scoreMap[GuidName] + 1;
                    scoreCache.scoreMap["ModifyFlag"] = 1;
                }
                else
                {
                    scoreCache.scoreMap[GuidName] = 1;
                    scoreCache.scoreMap["ModifyFlag"] = 1;
                }
            return true;
        }
    }

    /// <summary>
    /// 扩展类，主要有很多静态方法，获取单例
    /// </summary>
    public class ExtendMethord
    {
        private volatile static UrlCache urlcache = null;
        private volatile static ScoreCache scoreCache = null;
        private static readonly object lockHelper = new object();
        private static readonly object _lockHelper = new object();
        public static Queue<OperateScoreCache> OperateScoreCacheQueue = new Queue<OperateScoreCache>();

        /// <summary>
        /// 获取UrlCache的单例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UrlCache GetUrl()
        {
           if (urlcache == null)
           {
               lock (lockHelper)
               {
                   urlcache = new UrlCache();
               }
           }
           return urlcache;
        }
        /// <summary>
        /// 获取ScoreCache的单例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ScoreCache GetScore()
        {
            if (scoreCache == null)
            {
                lock (_lockHelper)
                {
                    scoreCache = new ScoreCache();
                }
            }
            return scoreCache;
        }

        public static string ExUrlCreate(string ID)
        {
            return ConfigurationManager.AppSettings["domain"].ToString() + "?strQuery=" + ID;
        }

        public static bool UpdateDB()
        {//一次性打包更新

            try {
                    var ScoreMapCach = ExtendMethord.GetScore().scoreMap;
                    if (ScoreMapCach["ModifyFlag"] == 0)
                    {
                        return false;
                    }
                    var temp = ScoreMapCach.Where(c => true);
                    DbContext db = DBContextFactory.CreateDbContext();

                    //@刘剑_1989: 更新是这样的，
                    //T existing = Context.Set<T>().Find
                    //如果 existing == null， Context.Set<T>().Add(item)；
                    //否则， 将 item 的值赋给 existing（不包括主键的值），
                    //最后，Context.SaveChanges

                    foreach (var item_ in temp)
                    {
                        var tempModels =  db.Set<ExtensionAgents>().Where<ExtensionAgents>(c => c.GUID == item_.Key);
                        var Scores = tempModels.Select(c => c.ExtensionScore).FirstOrDefault() + item_.Value;
                        foreach (var item in tempModels)
                        {
                            ExtensionAgents EA = db.Set<ExtensionAgents>().Find(item.ID);
                            if (EA != null)
                            {
                                EA.DelFlag = item.DelFlag;
                                EA.ExtensionScore = Scores;
                                EA.ID = item.ID;
                                EA.GUID = item.GUID;
                                EA.LName = item.LName;
                                EA.Remark = item.Remark;
                                EA.RouteStatisticsLinks_ID = item.RouteStatisticsLinks_ID;
                                EA.ModifiedOn = DateTime.Now;
                                EA.Sort = item.Sort;
                                EA.ExtensionUrl = item.ExtensionUrl;
                                EA.SubTime = item.SubTime;
                                EA.UrlName = item.UrlName;
                            }
                            else
                                return false;
                        }
                    }
                    db.SaveChanges();
                    List<string> list = new List<string>();
                    list.AddRange(ScoreMapCach.Keys);
                    foreach (var item in list)
                    {
                        ScoreMapCach[item] = 0;//计数以后清空
                    }
                    ScoreMapCach["ModifyFlag"] = 0;// 修改位置为0
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
