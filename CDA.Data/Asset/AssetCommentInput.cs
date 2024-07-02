using CDA.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class AssetCommentInput : BaseInput
    {
        public Guid AssetGuid { get; set; }
        public string Comment { get; set; }
    }
}
