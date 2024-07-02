using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Utilities
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
