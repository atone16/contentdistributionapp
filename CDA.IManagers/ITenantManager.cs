using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface ITenantManager
    {
        Task<TenantDto> CreateTenant(TenantInput input);
        Task<TenantDto> UpdateTenant(Guid id, TenantInput input);
        Task<TenantDto> GetById(Guid id);
    }
}
