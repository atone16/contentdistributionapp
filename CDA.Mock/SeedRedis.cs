using CDA.Data;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Mock
{
    public class SeedRedis
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public SeedRedis(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public void SeedData()
        {
            // Add Tenant
            var tenant = new Tenant
            {
                Id = Guid.Parse("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7"),
                TenantId = Guid.Parse("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7"),
                TenantName = "Joses Tenant"
            };
            _db.StringSet("Tenant::ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7", JsonConvert.SerializeObject(tenant));
            _db.SetAdd("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7::Tenant", "ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7");

            // Add User
            var user = new User
            {
                FirstName = "Jose",
                LastName = "Banzon",
                Email = "jose.banzon@email.com",
                TenantId = Guid.Parse("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7"),
            };
            _db.StringSet("User::1658272f-b742-4fcf-91fa-638575ca6503", JsonConvert.SerializeObject(tenant));
            _db.SetAdd("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7::User", "1658272f-b742-4fcf-91fa-638575ca6503");

            Console.WriteLine("Redis has been seeded.");
        }
    }
}

