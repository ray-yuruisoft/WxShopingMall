using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace T4Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var entity = new EntityClassInfo();
            foreach (var item in entity.EntitiesList)
            {
                Console.Write(item);
                Console.ReadKey();
            }
        }
    }
}
