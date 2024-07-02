using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Utilities
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid GenerateNewGuid()
        {
            return Guid.NewGuid();
        }
    }
}
