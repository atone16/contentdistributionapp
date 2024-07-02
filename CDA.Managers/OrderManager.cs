using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

namespace CDA.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderAccess orderAccess;
        private readonly IOrderBriefAccess orderBriefAccess;
        private readonly IMapper mapper;

        public OrderManager(
            IOrderAccess orderAccess,
            IOrderBriefAccess orderBriefAccess,
            IMapper mapper
        )
        {
            this.mapper = mapper;
            this.orderAccess = orderAccess;
            this.orderBriefAccess = orderBriefAccess;
        }

        public async Task<bool> ArchiveOrder(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var order = await this.orderAccess.GetByIdAsync(id);

                if (order.TenantId != tenantId)
                {
                    throw new Exception("Cant archive an order not within the same tenant");
                }

                order.IsArchived = true;
                order.LastUpdatedBy = userId;

                await this.orderAccess.UpdateAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error archiving order. {id}. {ex.Message}");
            }
        }

        public async Task<OrderDto> CreateOrder(OrderInput input)
        {
            var inputOrder = this.mapper.Map<Order>(input);
            inputOrder.CreatedBy = input.UserId;
            var createdOrder = await this.orderAccess.CreateAsync(inputOrder);
            return this.mapper.Map<OrderDto>(createdOrder);
        }

        public async Task<OrderDto> GetById(Guid id)
        {
            var brief = await this.orderAccess.GetByIdAsync(id);
            return this.mapper.Map<OrderDto>(brief);
        }

        public async Task<OrderDto> UpdateOrder(Guid id, OrderInput input)
        {
            try
            {
                var order = await this.orderAccess.GetByIdAsync(id);

                if (order == null)
                {
                    throw new Exception("Cannot Update A NonExistent Order.");
                }

                if (order.IsArchived)
                {
                    throw new Exception("Cannot Update An Archived Order.");
                }

                order.OrderNumber = input.OrderNumber ?? order.OrderNumber;
                order.RequesterUserId = input.RequesterUserId ?? order.RequesterUserId;
                order.OrderDate = input.OrderDate ?? order.OrderDate;
                order.CampaignName = input.CampaignName ?? order.CampaignName;
                order.LastUpdatedBy = input.UserId;

                var updatedOrder = await this.orderAccess.UpdateAsync(order);
                return this.mapper.Map<OrderDto>(updatedOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Order. {id}. {ex.Message}");
            }
        }

        public async Task<List<OrderDto>> GetByTenantId(Guid tenantId)
        {
            var users = await this.orderAccess.GetByTenantId(tenantId);
            return this.mapper.Map<List<OrderDto>>(users);
        }

        public async Task<OrderBriefDto> AddBriefToOrder(OrderBriefInput input)
        {
            var inputOrderBrief = this.mapper.Map<OrderBrief>(input);
            var createdOrderBrief = await this.orderBriefAccess.CreateAsync(inputOrderBrief);
            return this.mapper.Map<OrderBriefDto>(createdOrderBrief);
        }

        public async Task<bool> RemoveBriefToOrder(Guid id, Guid tenantId)
        {
            try
            {
                var orderBrief = await this.orderBriefAccess.GetByIdAsync(id);
                if (orderBrief.TenantId != tenantId)
                {
                    throw new Exception("Cannot Delete Order Brief if not same tenant");
                }

                return await this.orderBriefAccess.RemoveAsync(id, orderBrief.TenantId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting record. {ex.Message}");
            }
        }

        public async Task<List<OrderBriefDto>> GetOrderBriefsByOrderId(Guid orderId, Guid tenantId)
        {
            var orderBriefs = await this.orderBriefAccess.GetByOrderId(orderId, tenantId);
            return this.mapper.Map<List<OrderBriefDto>>(orderBriefs);
        }

        public async Task<OrderBriefDto> UpdateOrderBrief(Guid id, int quantity)
        {
            try
            {
                var orderBrief = await this.orderBriefAccess.GetByIdAsync(id);

                if (orderBrief == null)
                {
                    throw new Exception("Cannot Update A NonExistent Order.");
                }

                if (orderBrief.IsArchived)
                {
                    throw new Exception("Cannot Update An Archived Order.");
                }

                orderBrief.Quantity = quantity;

                var updatedOrderBrief = await this.orderBriefAccess.UpdateAsync(orderBrief);
                return this.mapper.Map<OrderBriefDto>(updatedOrderBrief);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Order Brief. {id}. {ex.Message}");
            }
        }
    }
}
