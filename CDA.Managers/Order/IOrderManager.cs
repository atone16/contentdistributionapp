using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public interface IOrderManager
    {
        Task<bool> ArchiveOrder(Guid id);
        Task<OrderDto> CreateOrder(OrderInput input);
        Task<OrderDto> GetById(Guid id);
        Task<OrderDto> UpdateOrder(Guid id, OrderInput input);
    }
}
