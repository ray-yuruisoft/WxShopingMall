using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;
using Newtonsoft.Json;
using Autofac;

namespace RS.Model.Json.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string strjson = System.IO.File.ReadAllText(@"C:\Users\Administrator\Desktop\2017.5.5\RS\RS\Yuruisoft.RS\Yuruisoft.RS.Web\Yuruisoft.RS.Config\runtimemodelconfig.json.txt");
            //RuntimeModelMeta p = Newtonsoft.Json.JsonConvert.DeserializeObject<RuntimeModelMeta>(strjson);
            //string strjson1 = "{\r\n\"Name\":\r\n\"苹果\",\"Expiry\":\"2014-05-03 10:20:59\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}";
            //Product p1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(strjson1);
            //Console.Write("{0}", strjson);
            //Console.ReadKey();

            //var builder = new ContainerBuilder();
            //builder.RegisterType<List<RuntimeModelMetaConfig>>().As<IList<RuntimeModelMetaConfig>>();
            //builder.Register(c => new DefaultRuntimeModelProvider(c.Resolve<IList<RuntimeModelMetaConfig>>()));//与前面RegisterType<>互斥

            //using (var container = builder.Build())
            //{
            //    var manager = container.Resolve<DefaultRuntimeModelProvider>();
            //}

            RuntimeModelMetaConfig R = new RuntimeModelMetaConfig();
            DefaultRuntimeModelProvider D = new DefaultRuntimeModelProvider(R);
            var x = D.GetType(1);

            Console.ReadKey();


        }
    }
    //public class Product
    //{
    //    public string Name { get; set; }
    //    public string Expiry { get; set; }
    //    public Decimal Price { get; set; }
    //    public string[] Sizes { get; set; }
    //}
    //public class RuntimeModelMeta
    //{
    //    public int ModelId { get; set; }
    //    public string ModelName { get; set; }//模型名称
    //    public string ClassName { get; set; }//类名称
    //    public ModelPropertyMeta[] ModelProperties { get; set; }

    //    public class ModelPropertyMeta
    //    {
    //        public string Name { get; set; }//对于的中文名字
    //        public string PropertyName { get; set; }//类属性名字
    //        public int Length { get; set; }//数据长度，主要用于string类型

    //        public bool IsRequired { get; set; }//是否必须输入，用于数据验证
    //        public string ValueType { get; set; }//数据类型，可以是字符串，日期，bool 等
    //    }
    //}
}
