using CDA.Data.Base;
using CDA.Data.Enum;

namespace CDA.Data
{
    public class ContentDistributionInput : BaseInput
    {
        public DateTime? DistributionDate { get; set; }
        public List<DistributionChannel> DistributionChannels { get; set; }
        public List<DistributionMethod> DistributionMethods { get; set; }
    }
}
