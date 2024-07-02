using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public static class UpdateValue
    {
        public static string GetUpdatedValue(string input, string actual)
        {
            return input != null ? (input != "" ? input : null) : actual;
        }
    }
}
