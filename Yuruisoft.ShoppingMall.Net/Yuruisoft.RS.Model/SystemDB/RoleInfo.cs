using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public partial class RoleInfo
    {

        public int ID { get; set; }
        public string RoleName { get; set; }
        public short DelFlag { get; set; }
        public System.DateTime SubTime { get; set; }
        public string Remark { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Sort { get; set; }


        public RoleInfo()
        {
            this.ActionInfo = new HashSet<ActionInfo>();
            this.UserInfo = new HashSet<UserInfo>();
        }

        public virtual ICollection<ActionInfo> ActionInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
