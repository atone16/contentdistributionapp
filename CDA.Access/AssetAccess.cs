using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.RedisCache;
using CDA.Utilities;

namespace CDA.Access
{
    public class AssetAccess : RedisCacheBaseAccess<Asset>, IAssetAccess
    {
        public AssetAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider) 
            : base(cache, guidGenerator, dateTimeProvider)
        {
        }
    }
}
