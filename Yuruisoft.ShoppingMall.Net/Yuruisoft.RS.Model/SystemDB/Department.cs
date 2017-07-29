using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public partial class Department
    {
        public int ID { get; set; }
        public string DepName { get; set; }
        public int ParentId { get; set; }
        public string TreePath { get; set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }

        public Department()
        {
            this.ActionInfo = new HashSet<ActionInfo>();
            this.UserInfo = new HashSet<UserInfo>();
        }
        public virtual ICollection<ActionInfo> ActionInfo { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
