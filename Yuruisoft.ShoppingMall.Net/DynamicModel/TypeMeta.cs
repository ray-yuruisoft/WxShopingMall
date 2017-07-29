using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModel
{
    public class TypeMeta
    {
        public TypeMeta()
        {
            PropertyMetas = new List<TypePropertyMeta>();
            AttributeMetas = new List<AttributeMeta>();
        }
        public Type BaseType { get; set; }
        public string TypeName { get; set; }
        public List<TypePropertyMeta> PropertyMetas { get; set; }
        public List<AttributeMeta> AttributeMetas { get; set; }

        public class TypePropertyMeta//属性
        {
            public TypePropertyMeta()
            {
                AttributeMetas = new List<AttributeMeta>();
            }
            public Type PropertyType { get; set; }
            public string PropertyName { get; set; }
            public List<AttributeMeta> AttributeMetas { get; set; }//属性的特性
        }

        public class AttributeMeta//特性
        {
            public Type AttributeType { get; set; }
            public Type[] ConstructorArgTypes { get; set; }
            public object[] ConstructorArgValues { get; set; }
            public string[] Properties { get; set; }
            public object[] PropertyValues { get; set; }
        }
    }
}
