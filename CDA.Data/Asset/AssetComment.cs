﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class AssetComment : BaseCommentData
    {
        public Guid AssetId { get; set; }
    }
}
