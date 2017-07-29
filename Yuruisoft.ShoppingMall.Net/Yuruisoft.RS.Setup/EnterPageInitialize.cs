using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model.RuntimeModel;

namespace Yuruisoft.RS.Setup
{
    public class EnterPageInitialize
    {
        //private static RuntimeModelMeta TempCache = null;
        private volatile static ConfigDataCache _instance = null;
        private static readonly object lockHelper = new object();
        private EnterPageInitialize() { }
        public static ConfigDataCache CreateInstance(ConfigDataProvider datas)
        {
            if (_instance == null || datas.IsEditFlag)
            {
                lock (lockHelper)
                {
                    _instance = new ConfigDataCache(datas);
                }
            }
            return _instance;
        }

        /// <summary>
        /// 修改报名页面显示的单例对象
        /// </summary>
        /// <param name="ConfigDatas"></param>
        /// <returns></returns>
        public static ConfigDataProvider ConfigProviderCreate(ConfigData[] ConfigDatas)
        {
            ConfigDataProvider datas = new ConfigDataProvider
            {
                IsEditFlag = true,
                ConfigDatas = ConfigDatas
            };
            return datas;
        }
        /// <summary>
        /// 初始化报名页面显示的单例对象
        /// </summary>
        /// <returns></returns>
        public static ConfigDataProvider ConfigProviderCreate()
        {
            ////序列化
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(datas, Newtonsoft.Json.Formatting.Indented);
            //string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/Yuruisoft.RS.Config/ConfigDataProvider.json.txt");
            //File.WriteAllText(path, json, Encoding.Default);

            //反序列化,通过读本地配置拿数据
            string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/Yuruisoft.RS.Config/ConfigDataProvider.json.txt");
            string strjson = System.IO.File.ReadAllText(path, Encoding.Default);
            ConfigDataProvider datas = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigDataProvider>(strjson);
            return datas;
        }
    }
}
