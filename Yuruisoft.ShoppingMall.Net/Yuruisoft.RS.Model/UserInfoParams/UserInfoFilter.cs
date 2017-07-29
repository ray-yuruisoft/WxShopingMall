using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.UserInfoParams
{
    /// <summary>
    /// 针对用户的搜索条件
    /// </summary>
    public class UserInfoFilter:BaseParam
    {
        public string UName { get; set; }
        public string URemark { get; set; }
        public string UEmail { get; set; }
        public string UPhoneNumber { get; set; }
    }
}
