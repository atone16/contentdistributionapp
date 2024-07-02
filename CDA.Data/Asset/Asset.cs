using CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class Asset : BaseData
    {
        public string AssetId { get; set; }
        public Status AssetStatus { get; set; }
        public Guid AssignedUserId { get; set; }
        public List<AssetVersion> AssetVersions { get; set; } = new List<AssetVersion>();
    }
}
