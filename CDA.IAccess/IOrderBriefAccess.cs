using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IOrderBriefAccess : IBaseAccess<OrderBrief>
    {
        Task<List<OrderBrief>> GetByOrderId(Guid orderId, Guid tenantId);
    }
}
