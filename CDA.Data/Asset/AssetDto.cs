using CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class AssetDto : BaseTenantDto
    {
        public Status AssetStatus { get; set; }
    }
}
