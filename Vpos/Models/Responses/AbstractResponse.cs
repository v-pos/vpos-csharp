using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Models
{
    public abstract class AbstractResponse
    {
        public int status;
        public string message;

        public bool ContainsErrors()
        {
            bool containsErrors = status >= 400;
            return containsErrors;
        }
    }
}
