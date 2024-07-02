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
        public UserAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider) 
            : base(cache, guidGenerator, dateTimeProvider)
        {
        }
    }
}
