using CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class AssetVersion : BaseTenantData
    {
        public int Number { get; set; } = 1;
        public Guid AssetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetFormat Format { get; set; }
        public AssetType Type { get; set; }
        public long Size { get; set; }
        public string FilePath { get; set; }
        public string Preview { get; set; }
    }
}
