using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.RuntimeModel
{
    public class ConfigDataCache
    {
         private Dictionary<string, string> _resultMap;
         private object _lock = new object();
         private volatile ConfigDataProvider _config = null;
         public ConfigDataCache(ConfigDataProvider datas)
         {
             _config = datas;
         }
         public Dictionary<string, string> Map_ConfigData
         {
             get
             {
                 if (_resultMap == null)
                 {
                     lock (_lock)
                     {
                         _resultMap = new Dictionary<string, string>();
                         foreach (var item in _config.ConfigDatas)
                         {//编译结果放到缓存中，方便下次使用                            
                             _resultMap.Add(item.TypeName, item.DataString);
                         }
                     }
                 }
                 return _resultMap;
             }
         }
    }
}
