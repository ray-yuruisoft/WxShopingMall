using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.UserInfoParams
{
    /// <summary>
    /// 针对推广员的搜索条件
    /// </summary>
    public class ExtensionAgentsFilter : BaseParam
    {
        public string LName { get; set; }
        public string Remark { get; set; }
    }
}
