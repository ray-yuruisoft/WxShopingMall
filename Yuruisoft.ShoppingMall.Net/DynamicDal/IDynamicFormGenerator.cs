using DynamicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDal
{
    public interface IDynamicFormGenerator
    {
        string Generate(object obj, IEnumerable<RuntimeModelMeta.ModelPropertyMeta> properties);
    }
    public interface IDynamicFormFieldGenerator
    {
        //根据传递的属性及对象，生成表单
        string Generate(object obj, RuntimeModelMeta.ModelPropertyMeta meta, bool onlyform = false);
        //这个表示当前的实现是针对哪一种ShowType的
        string ForType { get; }
    }
    public interface IDynamicFormFieldGeneratorProvider
    {
        IDynamicFormFieldGenerator Get(string type);
    }
}
