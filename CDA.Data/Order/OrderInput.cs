using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class OrderInput
    {
        public string OrderNumber { get; set; }
        public Guid? RequesterUserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CampaignName { get; set; }
    }
}
