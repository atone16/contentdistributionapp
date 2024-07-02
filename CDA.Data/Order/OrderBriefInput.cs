using CDA.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class OrderBriefInput : BaseInput
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public string BriefId { get; set; }
    }
}
