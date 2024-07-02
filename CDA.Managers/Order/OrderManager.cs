using AutoMapper;
using CDA.Data;
using CDA.IAccess;

namespace CDA.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderAccess orderAccess;
        private readonly IMapper mapper;

        public OrderManager(
            IOrderAccess orderAccess,
            IMapper mapper
        ) 
        { 
            this.mapper = mapper;
            this.orderAccess = orderAccess;
        }

        public async Task<bool> ArchiveOrder(Guid id)
        {
            try
            {
                var order = await this.orderAccess.GetByIdAsync(id);
                order.IsArchived = true;
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

                var updatedOrder = await this.orderAccess.UpdateAsync(order);
                return this.mapper.Map<OrderDto>(updatedOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Order. {id}. {ex.Message}");
            }
        }

    }
}
