using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface IOrderManager
    {
        Task<bool> ArchiveOrder(Guid id, Guid tenantId, Guid userId);
        Task<OrderDto> CreateOrder(OrderInput input);
        Task<OrderDto> GetById(Guid id);
        Task<OrderDto> UpdateOrder(Guid id, OrderInput input);
        Task<List<OrderDto>> GetByTenantId(Guid tenantId);
        Task<OrderBriefDto> AddBriefToOrder(OrderBriefInput input);
        Task<List<OrderBriefDto>> GetOrderBriefsByOrderId(Guid orderId, Guid tenantId);
        Task<bool> RemoveBriefToOrder(Guid id, Guid tenantId);
        Task<OrderBriefDto> UpdateOrderBrief(Guid id, int quantity);
    }
}
