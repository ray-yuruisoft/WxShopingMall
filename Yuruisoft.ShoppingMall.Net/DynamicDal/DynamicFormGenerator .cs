using DynamicModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDal
{
    public class DynamicFormGenerator : IDynamicFormGenerator
    {
        private readonly IDynamicFormFieldGeneratorProvider _fieldGeneratorProvider;
        public DynamicFormGenerator(IDynamicFormFieldGeneratorProvider provider)
        {
            _fieldGeneratorProvider = provider;
        }
        public string Generate(object obj, IEnumerable<DynamicModel.RuntimeModelMeta.ModelPropertyMeta> properties)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in properties)
            {
                //根据showtype获取表单构造器
                IDynamicFormFieldGenerator fieldGenerator = _fieldGeneratorProvider.Get(item.ShowType);
                builder.Append(fieldGenerator.Generate(obj,item));
            }
            return builder.ToString();
        }
    }

    public class CheckboxFieldGenerator : IDynamicFormFieldGenerator
    {
        public string ForType
        {
           get{ return "checkbox"; }
        }
        public string Generate(object obj, RuntimeModelMeta.ModelPropertyMeta meta,bool onlyform=false)
        {
            //把动态对象转换成一个DynamicEntity，为的是后面获取数据方便，因为DynamicEntity支持通过索引获取属性数据
            DynamicEntity entity = obj as DynamicEntity;
            if (obj == null)
            {
                throw new NullReferenceException("DynamicEntity");
            }
            //通过entity[meta.PropertyName]获取到属性数据
            return string.Format("<input id='小贝' name='小贝' type='checkbox' value='{0}'/>",meta.PropertyName);
        }
    }
    public class InputFieldGenerator : IDynamicFormFieldGenerator
    {
        public string ForType
        {
            get { return "input"; }
        }
        public string Generate(object obj, RuntimeModelMeta.ModelPropertyMeta meta, bool onlyform = false)
        {
            //把动态对象转换成一个DynamicEntity，为的是后面获取数据方便，因为DynamicEntity支持通过索引获取属性数据
            DynamicEntity entity = obj as DynamicEntity;
            if (obj == null)
            {
                throw new NullReferenceException("DynamicEntity");
            }
            //通过entity[meta.PropertyName]获取到属性数据

           var htmlString = @"
                        <tr>
                             <td><label for='{0}'>{1}</label>：</td>
                             <td>
                                 <input id='{0}' name='{0}' class='easyui-validatebox textbox' data-options='required:{2}'>
                             </td>
                        </tr>";
           return string.Format(htmlString, meta.PropertyName, meta.Name,meta.IsRequired.ToString().ToLower());
        }
    }


    public class DynamicFormFieldGeneratorProvider : IDynamicFormFieldGeneratorProvider
    {
        public static List<IDynamicFormFieldGenerator> GetDynamicFormFieldGenerators()
        {//存入信号槽，比做单例要好
            List<IDynamicFormFieldGenerator> generatorsCache = (List<IDynamicFormFieldGenerator>)CallContext.GetData("DynamicFormFieldGeneratorsCache");
            if (generatorsCache == null)
            {
                generatorsCache = new List<IDynamicFormFieldGenerator>();
                IDynamicFormFieldGenerator checkboxfieldgenerator = new CheckboxFieldGenerator();//这里每增加一种类型，都来要注册
                IDynamicFormFieldGenerator inputFieldGenerator = new InputFieldGenerator();

                generatorsCache.Add(checkboxfieldgenerator);//这里每增加一种类型，都来要注册
                generatorsCache.Add(inputFieldGenerator);


                CallContext.SetData("DynamicFormFieldGeneratorsCache", generatorsCache);
            }
            return generatorsCache;
        }

        private IEnumerable<IDynamicFormFieldGenerator> _generators;
        public DynamicFormFieldGeneratorProvider()
        {
            _generators = GetDynamicFormFieldGenerators();
        }
        public IDynamicFormFieldGenerator Get(string type)
        {
            if (_generators == null)
            {
                _generators = GetDynamicFormFieldGenerators();
            }
            if (_generators == null)
            {
                throw new NotSupportedException("IDynamicFormFieldGenerator");
            }
            //根据type找到第一个符合条件的构造器，并返回
            IDynamicFormFieldGenerator g = _generators.FirstOrDefault(m => m.ForType == type);
            if (g == null)
            {
                throw new NotSupportedException("not supproted for " + type + "'s form field generator");
            }
            return g;
        }
    }




}
