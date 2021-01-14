using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Models
{
    public class LocationResponse : AbstratcResponse
    {
        public string location;

        public string GetRequestID()
        {
            return location.Replace("/api/v1/requests/", "").Replace("/api/v1/transactions/", "");
        }
    }
}
