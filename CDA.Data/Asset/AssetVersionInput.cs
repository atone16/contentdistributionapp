using CDA.Data.Base;
using CDA.Data.Enum;

namespace CDA.Data
{
    public class AssetVersionInput : BaseInput
    {
        public Guid AssetGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetFormat Format { get; set; }
        public AssetType Type { get; set; }
        public long Size { get; set; }
        public string FilePath { get; set; }
        public string Preview { get; set; }
    }
}
