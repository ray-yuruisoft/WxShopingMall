using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.EnterPagePortfolioModal
{
    [Serializable]
    public class EnterFormFieldModul
    {
        public string LabelName { get; set; }
        public string TypeName { get; set; }
        public string PlaceholderString { get; set; }
        public string Attribute { get; set; }
        public string DisplayName { get; set; }
    }
}
