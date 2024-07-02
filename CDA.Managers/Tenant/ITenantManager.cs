using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public interface ITenantManager
    {
        Task<TenantDto> CreateTenant(TenantInput input);
        Task<TenantDto> UpdateTenant(Guid id, TenantInput input);
        Task<bool> ArchiveTenant(Guid id);
        Task<TenantDto> GetById(Guid id);
    }
}
