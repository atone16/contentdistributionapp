using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IBaseAccess<TItem> where TItem : BaseData
    {
        Task<TItem> CreateAsync(TItem input);
        Task<TItem> UpdateAsync(TItem item);
        Task<TItem> GetByIdAsync(Guid itemId);
        Task<List<TItem>> GetByTenantId(Guid tenantId);
        Task<bool> RemoveAsync(Guid itemId, Guid tenantId);
    }
}
