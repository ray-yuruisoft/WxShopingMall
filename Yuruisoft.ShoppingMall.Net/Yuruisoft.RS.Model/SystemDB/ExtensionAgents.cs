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
    public partial class ExtensionAgents
    {
        [Key, DisplayName("ID")]
        public int ID { get; set; }

        [ DisplayName("GUID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "* GUID不能为空")]
        public string GUID { get; set; }

        [DisplayName("推广员名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "* 推广员名不能为空")]
        //[System.Web.Mvc.Remote("CheckUserName", "UserInfo", ErrorMessage = "* 推广员名已存在")]
        public string LName { get; set; }

        [Required(ErrorMessage = "* 需要推广的链接名不能为空"), DisplayName("需要推广的链接名")]
        public string UrlName { get; set; }

        [Required(ErrorMessage = "* 发送给推广员的链接不能为空"), DisplayName("发送给推广员的链接")]
        [RegularExpression(@"[a-zA-z]+://[^\s]*", ErrorMessage = "* 发送给推广员的链接格式不正确")]
        public string ExtensionUrl { get; set; }

        [Display(Name = "推广合格次数")]
        public long ExtensionScore { get; set; }

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

        [Required,DisplayName("链接表的逻辑外键")]
        public int RouteStatisticsLinks_ID { get; set; }


        //public virtual RouteStatisticsLinks RouteStatisticsLinks { get; set; }
    }
}
