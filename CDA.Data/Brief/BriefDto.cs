using CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class BriefDto : BaseTenantDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssetId { get; set; }
        public Status Status { get; set; }
    }
}
