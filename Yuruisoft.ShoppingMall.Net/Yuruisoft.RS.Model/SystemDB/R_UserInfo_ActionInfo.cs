using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public partial class R_UserInfo_ActionInfo
    {
        public int ID { get; set; }
        public int UserInfoID { get; set; }
        public int ActionInfoID { get; set; }
        public bool IsPass { get; set; }

        public virtual ActionInfo ActionInfo { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
