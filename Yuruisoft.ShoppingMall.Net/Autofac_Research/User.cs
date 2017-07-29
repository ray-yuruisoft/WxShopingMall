using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac_Research
{
    public interface Identity
    {
        int Id { get; set; }
    }
    public class User : Identity
    {
        public int Id   { get; set;}
        public string Name { get; set; }
    }

}
