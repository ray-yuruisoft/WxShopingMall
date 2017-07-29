using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DAL;

namespace Yuruisoft.RS.DALfactory
{
    /// <summary>
    /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
    /// </summary>
    public partial class DALAbstractFactory
    {
        private static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];
        private static readonly string DalAssembly = ConfigurationManager.AppSettings["DalAssembly"];
        private static object CreateInstance(string fullClassName, string assemblyPath)
        {
            var assembly = Assembly.Load(assemblyPath);//加载程序集
            return assembly.CreateInstance(fullClassName);
        }
    }
}
