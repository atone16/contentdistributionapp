using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IAssetCommentAccess : IBaseAccess<AssetComment>
    {
        Task<List<AssetComment>> GetByAssetId(Guid assetGuid, Guid tenantId);
    }
}
