using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class BriefCommentInput
    {
        public Guid BriefId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
    }
}
