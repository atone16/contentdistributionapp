using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.RedisCache;
using CDA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Access
{
    public class TenantAccess : RedisCacheBaseAccess<Tenant>, ITenantAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;

        public TenantAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider) 
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
        }

        public override Task<Tenant> CreateAsync(Tenant input)
        {
            var guidId = guidGenerator.GenerateNewGuid();
            input.Id = guidId;
            input.TenantId = guidId;
            input.CreatedDate = dateTimeProvider.UtcNow;
            return base.CreateAsync(input);
        }

        public override Task<Tenant> UpdateAsync(Tenant item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }
    }
}
