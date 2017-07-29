using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.IDAL
{
    public partial interface IDBSession
    {
        /// <summary>
        /// 保存数据使用的
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();//项目架构亮点
    }
}
