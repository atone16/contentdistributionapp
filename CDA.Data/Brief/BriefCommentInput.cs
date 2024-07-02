using CDA.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class BriefCommentInput : BaseInput
    {
        public Guid BriefGuid { get; set; }
        public string Comment { get; set; }
    }
}
