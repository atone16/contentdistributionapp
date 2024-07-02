﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class OrderBriefDto : BaseTenantDto
    {
        public Guid OrderId { get; set; }
        public string BriefId { get; set; }
        public int Quantity { get; set; }
    }
}
