using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    [Serializable]
    public partial class RouteStatisticsLinks
    {
        [Key, DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("链接名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "* 链接名不能为空")]
        //[System.Web.Mvc.Remote("CheckUserName", "UserInfo", ErrorMessage = "* 链接名字已存在")]
        public string LName { get; set; }

        [Required(ErrorMessage = "* 链接不能为空"), DisplayName("链接")]
        [RegularExpression(@"[a-zA-z]+://[^\s]*", ErrorMessage = "* 链接的格式不正确")]
        public string Url { get; set; }

        [Display(Name = "备注")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Required, DisplayName("删除标记")]
        [RegularExpression(@"^[0-2]$", ErrorMessage = "* 只能输入0-2的数字")]
        public short DelFlag { get; set; }

        public string Sort { get; set; }

        [Required, DisplayName("创建时间")]
        public System.DateTime SubTime { get; set; }

        [Required, DisplayName("修改时间")]
        public System.DateTime ModifiedOn { get; set; }


        //public RouteStatisticsLinks()
        //{
        //    this.ExtensionAgents = new HashSet<ExtensionAgents>();
        //}
        //public virtual ICollection<ExtensionAgents> ExtensionAgents { get; set; }

    }
}
