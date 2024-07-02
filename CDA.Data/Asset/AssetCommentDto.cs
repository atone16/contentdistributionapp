using CDA.Data.Base;

namespace CDA.Data
{
    public class AssetCommentDto : BaseCommentDto
    {
        public Guid AssetGuid { get; set; }
    }
}
