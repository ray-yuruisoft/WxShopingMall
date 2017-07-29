using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Yuruisoft.RS.Model
{
    [Serializable]
    public partial class UserInfo
    {
        [Key,DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("账号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "* 账号不能为空")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "* 账号长度必须在{2}和{1}位之间")]
        [System.Web.Mvc.Remote("CheckUserName", "UserInfo", ErrorMessage = "* 账号已存在")]
        public string UName { get; set; }

        [DisplayName("密码")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "* 密码不能为空")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "* 密码长度必须在{2}和{1}位之间")]
        public string UPwd { get; set; }

        [DataType(DataType.Password)]
        [Compare("UPwd", ErrorMessage = "* 密码不一致")]
        [DisplayName("确认密码")]
        public string TUPwd { get; set; }

        [Required(ErrorMessage = "* 电子邮箱不能为空"), StringLength(255, ErrorMessage = "* 请勿输入超过255个字"), DisplayName("电子邮箱")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$", ErrorMessage = "* 电子邮箱的格式不正确")]
        public string UEmail { get; set; }

        [Display(Name = "手机号码")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "* 手机号码不能为空"), RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "* 手机号码格式不正确")]
        public long UPhoneNumber { get; set; }

        //[DisplayName("身份证")]
        //[RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = "身份证号码格式错误")]
        //public string IdentityNo { get; set; }

        //[DisplayName("年龄")]
        ////[Required(AllowEmptyStrings = false, ErrorMessage = "年龄不能为空")]
        //[Range(10, 120, ErrorMessage = "您输入的年龄不符合规范，年龄应该在{1}-{2}之间")]
        //public int Age { get; set; }

        //[Display(Name = "生日")]
        //[DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="yyyy/MM/dd")]  
        ////[Required] //当前字段的值不能为空  
        //public DateTime Birthday { get; set; } //生日  

        [Required,DisplayName("创建时间")]
        public System.DateTime SubTime { get; set; }

        [Required,DisplayName("修改时间")]
        public System.DateTime ModifiedOn { get; set; }

        [Display(Name = "备注")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        [Required,DisplayName("删除标记")]
        [RegularExpression(@"^[0-2]$", ErrorMessage = "* 只能输入0-2的数字")]
        public short DelFlag { get; set; }
        public string Sort { get; set; }




        public UserInfo()
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
