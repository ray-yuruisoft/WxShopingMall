using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.IBLL
{
     public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        bool DeleteEntities(List<int>list);
    }
}
