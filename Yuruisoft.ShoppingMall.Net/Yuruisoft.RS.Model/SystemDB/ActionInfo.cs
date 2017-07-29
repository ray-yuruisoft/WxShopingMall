using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public partial class  ActionInfo
    {
        public int ID { get; set; }
        public string ActionInfoName { get; set; }
        public string ControllerName { get; set; }
        public string ActionMethodName { get; set; }
        public string Url { get; set; }
        public short ActionTypeEnum { get; set; }
        public string MenuIcon { get; set; }
        public int IconWidth { get; set; }
        public int IconHeight { get; set; }
        public string HttpMethod { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
        public short DelFlag { get; set; }
        public string Sort { get; set; }
        //public string Test { get; set; }

        public ActionInfo()
        {
            this.R_UserInfo_ActionInfo = new HashSet<R_UserInfo_ActionInfo>();
            this.Department = new HashSet<Department>();
            this.RoleInfo = new HashSet<RoleInfo>();
        }
        public virtual ICollection<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
    }
}
