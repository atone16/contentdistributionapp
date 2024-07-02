using CDA.Data.Base;

namespace CDA.Data
{
    public class ContentDistributionAssetInput : BaseInput
    {
        public Guid ContentDistributionId { get; set; }
        public string AssetId { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
    }
}
