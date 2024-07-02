using CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class ContentDistributionDto : BaseTenantDto
    {
        public DateTime? DistributionDate { get; set; }
        public List<DistributionChannel> DistributionChannels { get; set; }
        public List<DistributionMethod> DistributionMethods { get; set; }
    }
}
