using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.EnterPagePortfolioModal
{
    [Serializable]
    public class portfolioModal
    {
        public string Name { get; set; }
        public string ImgSrc { get; set; }
        public string TitleInside { get; set; }
        public string ImgSrcInside { get; set; }
        public string paragraphInside { get; set; }
        public string LinkNameInside { get; set; }
        public string LinkContentInside { get; set; }
        public string LinkInside { get; set; }
    }
}
