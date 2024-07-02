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
    public class UserAccess : RedisCacheBaseAccess<User>, IUserAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;

        public UserAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider) 
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
        }

        public override Task<User> CreateAsync(User input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;
            return base.CreateAsync(input);
        }

        public override Task<User> UpdateAsync(User item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }
    }
}
