﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Data
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; } = Guid.Empty;
        public Guid? LastUpdatedBy { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
