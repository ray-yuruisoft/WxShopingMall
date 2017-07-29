using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DynamicModel
{
    public class DefaultRuntimeModelProvider:IRuntimeModelProvider
    {
        private Dictionary<int, Type> _resultMap;
        private readonly RuntimeModelMetaConfig _config;
        private object _lock = new object();

        public DefaultRuntimeModelProvider(RuntimeModelMetaConfig config) //需修改
        {
            //通过依赖注入方式获取到模型配置信息 暂时通过读文件
             _config = config;
        }
        //动态编译结果的缓存，这样在获取动态类型时不用每次都编译一次
        public Dictionary<int, Type> Map
        {
            get
            {
                if (_resultMap == null)
                {
                    lock (_lock)
                    {
                        _resultMap = new Dictionary<int, Type>();

                        foreach (var item in _config.Metas)
                        {
                            //根据RuntimeModelMeta编译成类，具体实现看后面内容                             
                             var result = RuntimeTypeBuilder.Build(GetTypeMetaFromModelMeta(item));                                                                                           _resultMap.Add(item.ModelId, result);                                              
                            //编译结果放到缓存中，方便下次使用                            
                        }
                    }
                }
                return _resultMap;
            }
        }

        public DynamicModel.RuntimeModelMeta[] GetRuntimeModelMeta()
        {
            return _config.Metas;
        }

        public Type GetType(int modelId)
        {
            Dictionary<int, Type> map = Map;
            Type result = null;
            if (!map.TryGetValue(modelId, out result))
            {
                throw new NotSupportedException("dynamic model not supported:" + modelId);
            }
            return result;
        }

        Type[] IRuntimeModelProvider.GetType()//获取所有
        {
            int[] modelIds = _config.Metas.Select(m => m.ModelId).ToArray();
            return Map.Where(m => modelIds.Contains(m.Key)).Select(m => m.Value).ToArray();
        }

        //这个方法就是把一个RuntimeModelMeta转换成更接近类结构的TypeMeta对象
        private TypeMeta GetTypeMetaFromModelMeta(RuntimeModelMeta meta)
        {
            TypeMeta typeMeta = new TypeMeta();
            //我们让所有的动态类型都继承自DynamicEntity类，这个类主要是为了方便属性数据的读取，具体代码看后面
            typeMeta.BaseType = typeof(DynamicEntity);
            typeMeta.TypeName = meta.ClassName;

            foreach (var item in meta.ModelProperties)
            {
                TypeMeta.TypePropertyMeta pmeta = new TypeMeta.TypePropertyMeta();
                pmeta.PropertyName = item.PropertyName;
                //如果必须输入数据，我们在属性上增加RequireAttribute特性，这样方便我们进行数据验证
                if (item.IsRequired)
                {
                    TypeMeta.AttributeMeta am = new TypeMeta.AttributeMeta();
                    am.AttributeType = typeof(RequiredAttribute);
                    am.Properties = new string[] { "ErrorMessage" };
                    am.PropertyValues = new object[] { "请输入" + item.Name };
                    pmeta.AttributeMetas.Add(am);
                }

                if (item.ValueType == "string")
                {
                    pmeta.PropertyType = typeof(string);
                    TypeMeta.AttributeMeta am = new TypeMeta.AttributeMeta();
                    //增加长度验证特性
                    am.AttributeType = typeof(StringLengthAttribute);
                    am.ConstructorArgTypes = new Type[] { typeof(int) };
                    am.ConstructorArgValues = new object[] { item.Length };
                    am.Properties = new string[] { "ErrorMessage" };
                    am.PropertyValues = new object[] { item.Name + "长度不能超过" + item.Length.ToString() + "个字符" };

                    pmeta.AttributeMetas.Add(am);
                }
                else if (item.ValueType == "int")
                {
                    if (!item.IsRequired)
                    {
                        pmeta.PropertyType = typeof(int?);
                    }
                    else
                    {
                        pmeta.PropertyType = typeof(int);
                    }
                }
                else if (item.ValueType == "datetime")
                {
                    if (!item.IsRequired)
                    {
                        pmeta.PropertyType = typeof(DateTime?);
                    }
                    else
                    {
                        pmeta.PropertyType = typeof(DateTime);
                    }
                }
                else if (item.ValueType == "bool")
                {
                    if (!item.IsRequired)
                    {
                        pmeta.PropertyType = typeof(bool?);
                    }
                    else
                    {
                        pmeta.PropertyType = typeof(bool);
                    }
                }
                typeMeta.PropertyMetas.Add(pmeta);
            }
            return typeMeta;
        }
    }

    public class DynamicEntity 
    {
        private Dictionary<object, object> _attrs;
        public DynamicEntity()
        {
            _attrs = new Dictionary<object, object>();
        }
        public DynamicEntity(Dictionary<object, object> dic)
        {
            _attrs = dic;
        }
        public static DynamicEntity Parse(object obj)
        {
            DynamicEntity model = new DynamicEntity();
            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                model._attrs.Add(info.Name, info.GetValue(obj, null));
            }
            return model;
        }
        public T GetValue<T>(string field)
        {
            object obj2 = null;
            if (!_attrs.TryGetValue(field, out obj2))
            {
                _attrs.Add(field, default(T));
            }
            if (obj2 == null)
            {
                return default(T);
            }
            return (T)obj2;
        }

        public void SetValue<T>(string field, T value)
        {
            if (_attrs.ContainsKey(field))
            {
                _attrs[field] = value;
            }
            else
            {
                _attrs.Add(field, value);
            }
        }

        [JsonIgnore]
        public Dictionary<object, object> Attrs
        {
            get
            {
                return _attrs;
            }
        }
        //提供索引方式操作属性值
        public object this[string key]
        {
            get
            {
                object obj2 = null;
                if (_attrs.TryGetValue(key, out obj2))
                {
                    return obj2;
                }
                return null;
            }
            set
            {
                if (_attrs.Any(m => string.Compare(m.Key.ToString(), key, true) != -1))
                {
                    _attrs[key] = value;
                }
                else
                {
                    _attrs.Add(key, value);
                }
            }
        }
        [JsonIgnore]
        public string[] Keys
        {
            get
            {
                return _attrs.Keys.Select(m => m.ToString()).ToArray();
            }
        }

        public int Id
        {
            get
            {
                return GetValue<int>("Id");
            }
            set
            {
                SetValue("Id", value);
            }
        }
        [Timestamp]
        [JsonIgnore]
        public byte[] Version { get; set; }
    }


    public static class RuntimeTypeBuilder
    {
        private static ModuleBuilder moduleBuilder;
        static RuntimeTypeBuilder()
        {
            AssemblyName an = new AssemblyName("__RuntimeType");
            moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run).DefineDynamicModule("__RuntimeType");
        }
        public static Type Build(TypeMeta meta)
        {//需增加一个判断
            TypeBuilder builder = moduleBuilder.DefineType(meta.TypeName, TypeAttributes.Public);
            CustomAttributeBuilder tableAttributeBuilder = new CustomAttributeBuilder(typeof(TableAttribute).GetConstructor(new Type[1] { typeof(string) }), new object[] { "RuntimeModel_" + meta.TypeName });
            builder.SetParent(meta.BaseType);
            builder.SetCustomAttribute(tableAttributeBuilder);

            foreach (var item in meta.PropertyMetas)
            {
                AddProperty(item, builder, meta.BaseType);
            }
            return builder.CreateTypeInfo().UnderlyingSystemType;
        }

        private static void AddProperty(TypeMeta.TypePropertyMeta property, TypeBuilder builder, Type baseType)
        {
            PropertyBuilder propertyBuilder = builder.DefineProperty(property.PropertyName, PropertyAttributes.None, property.PropertyType, null);

            foreach (var item in property.AttributeMetas)
            {
                if (item.ConstructorArgTypes == null)
                {
                    item.ConstructorArgTypes = new Type[0];
                    item.ConstructorArgValues = new object[0];
                }
                ConstructorInfo cInfo = item.AttributeType.GetConstructor(item.ConstructorArgTypes);
                PropertyInfo[] pInfos = item.Properties.Select(m => item.AttributeType.GetProperty(m)).ToArray();
                CustomAttributeBuilder aBuilder = new CustomAttributeBuilder(cInfo, item.ConstructorArgValues, pInfos, item.PropertyValues);
                propertyBuilder.SetCustomAttribute(aBuilder);
            }

            MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public;
            MethodBuilder getMethodBuilder = builder.DefineMethod("get_" + property.PropertyName, attributes, property.PropertyType, Type.EmptyTypes);
            ILGenerator iLGenerator = getMethodBuilder.GetILGenerator();
            MethodInfo getMethod = baseType.GetMethod("GetValue").MakeGenericMethod(new Type[] { property.PropertyType });
            iLGenerator.DeclareLocal(property.PropertyType);
            iLGenerator.Emit(OpCodes.Nop);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldstr, property.PropertyName);
            iLGenerator.EmitCall(OpCodes.Call, getMethod, null);
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.Emit(OpCodes.Ret);
            MethodInfo setMethod = baseType.GetMethod("SetValue").MakeGenericMethod(new Type[] { property.PropertyType });
            MethodBuilder setMethodBuilder = builder.DefineMethod("set_" + property.PropertyName, attributes, null, new Type[] { property.PropertyType });
            ILGenerator generator2 = setMethodBuilder.GetILGenerator();
            generator2.Emit(OpCodes.Nop);
            generator2.Emit(OpCodes.Ldarg_0);
            generator2.Emit(OpCodes.Ldstr, property.PropertyName);
            generator2.Emit(OpCodes.Ldarg_1);
            generator2.EmitCall(OpCodes.Call, setMethod, null);
            generator2.Emit(OpCodes.Nop);
            generator2.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

        }


    }

}
