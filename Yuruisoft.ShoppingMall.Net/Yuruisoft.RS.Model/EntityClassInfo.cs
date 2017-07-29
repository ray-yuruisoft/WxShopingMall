using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public class EntityClassInfo
    {
        public EntityClassInfo()
        {
            List<string> classNameList = new List<string>();
            PropertyInfo[] properties = typeof(Yuruisoft_DBContext).GetProperties();    // 获得对象所有属性
            foreach (var property in properties)
            {
                string propertyType = property.PropertyType.Name;   // 获得属性类型名称
                if (propertyType.Contains("DbSet"))     // 判断是否为实体集合
                {
                    Type[] genericTypes = property.PropertyType.GenericTypeArguments;   // 获得泛型类型数组
                    foreach (var type in genericTypes)
                    {
                        classNameList.Add(type.Name);   // 获得泛型类型名称 并添加到集合中
                    }
                }
            }
            this.EntitiesList = classNameList;
        }
        public List<string> EntitiesList { get; set; }
    }
}
