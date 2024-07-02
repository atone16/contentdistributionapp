using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data.Base
{
    public class BaseInput
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
