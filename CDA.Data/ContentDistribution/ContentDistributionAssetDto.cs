using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class ContentDistributionAssetDto : BaseTenantDto
    {
        public Guid ContentDistributionId { get; set; }
        public string AssetId { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
    }
}
