using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;

namespace DynamicModel
{
    public class SingletonForDymicModel
    {
         private static RuntimeModelMeta TempCache = null;
         private volatile static IRuntimeModelProvider _instance = null;
         private static readonly object lockHelper = new object();
         private SingletonForDymicModel() { }
         public static IRuntimeModelProvider CreateInstance(string strjson)
         {
           RuntimeModelMeta Temp = Newtonsoft.Json.JsonConvert.DeserializeObject<RuntimeModelMeta>(strjson);
           if (_instance == null || !CompareRuntimeMeta(Temp, TempCache))
           {
               TempCache = Temp;
               lock (lockHelper)
               {
                 _instance = new DefaultRuntimeModelProvider(new RuntimeModelMetaConfig(){Metas = new RuntimeModelMeta[] {TempCache}});
               }
           }
           return _instance;
         }
        /// <summary>
        /// 这里有个问题是：如果不修改类的名字，则重新创建时会出现同名程序集则不能创建，所以有任何更新都必须更新类名。但是更新了类名就无法做到数据迁移
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CompareRuntimeMeta(RuntimeModelMeta a,RuntimeModelMeta b)
        {
            if(a.ModelId != b.ModelId)
                return false;
            if(a.ModelName != b.ModelName)
                return false;
            if(a.ClassName != b.ClassName)
                return false;
            if(a.ModelProperties.Length != b.ModelProperties.Length)
                return false;
            for(int Count = 0 ; Count<a.ModelProperties.Length;Count++)
            {
                bool Isthere = false;
                for(int num = 0; num < b.ModelProperties.Length; num++)
                {
                    if(a.ModelProperties[Count].Name != b.ModelProperties[Count].Name)
                        break;
                    if (a.ModelProperties[Count].PropertyName != b.ModelProperties[Count].PropertyName)
                        break;
                    if (a.ModelProperties[Count].Length != b.ModelProperties[Count].Length)
                        break;
                    if (a.ModelProperties[Count].IsRequired != b.ModelProperties[Count].IsRequired)
                        break;
                    if (a.ModelProperties[Count].ValueType != b.ModelProperties[Count].ValueType)
                        break;
                    Isthere = true;
                }
                if (Isthere == false)
                    return false;
            }
            return true;
        }
    }
}
