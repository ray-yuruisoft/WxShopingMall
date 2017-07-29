using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emit_DynamicClassBuild
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeCreator tc = new TypeCreator(typeof(IAnimal));
            Type t = tc.build();
            IAnimal animal = (IAnimal)Activator.CreateInstance(t);
            animal.move();//这里没有定义animal类，完全动态创建
            animal.eat();
            Console.Read();
        }
    }
}
