using CDA.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class BriefCommentDto : BaseCommentDto
    {
        public Guid BriefGuid { get; set; }
    }
}
