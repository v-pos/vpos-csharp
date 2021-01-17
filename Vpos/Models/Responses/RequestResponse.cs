using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    public partial class RequestResponse : AbstractResponse
    {
        public Request data;
    }
}
