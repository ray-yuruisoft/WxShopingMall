using Yuruisoft.RS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.IBLL
{
   public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
       bool SetActionRoleInfo(int actionId,List<int>list);
       bool DeleteEntities(List<int> list);
    }
}
