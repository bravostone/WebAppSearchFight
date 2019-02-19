using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Entity
{
    public class BingEntity
    {
        public class WebPages
        {
            public string webSearchUrl { get; set; }
            public string webSearchUrlPingSuffix { get; set; }
            public int totalEstimatedMatches { get; set; }
        }
        public class BingObject
        {
            public WebPages webPages { get; set; }
        }
    }
}
