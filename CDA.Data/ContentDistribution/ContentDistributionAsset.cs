using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class ContentDistributionAsset : BaseTenantData
    {
        public Guid ContentDistributionId { get; set; }
        public Guid AssetId { get; set; }
        public string AssetDescription { get; set; }
    }
}
