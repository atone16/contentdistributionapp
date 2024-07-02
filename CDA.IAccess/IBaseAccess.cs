using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IBaseAccess<TItem>
        where TItem : BaseData
    {
        Task<TItem> CreateAsync(TItem input);
        Task<List<TItem>> CreateManyAsync(List<TItem> inputs);
        Task<TItem> UpdateAsync(TItem item);
        Task<bool> RemoveAsync(Guid itemId);
        Task<bool> RemoveManyAsync(List<TItem> inputs);
        Task<TItem> GetByIdAsync(Guid itemId);
        Task<List<TItem>> GetManyAsync(List<Guid> itemIds);
    }
}
