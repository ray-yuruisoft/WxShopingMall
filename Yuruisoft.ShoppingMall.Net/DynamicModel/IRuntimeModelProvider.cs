using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicModel
{
    public interface IRuntimeModelProvider
    {
        Type GetType(int modelId);
        Type[] GetType();
        DynamicModel.RuntimeModelMeta[] GetRuntimeModelMeta();
    }
}
