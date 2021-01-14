using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    public class TransactionsResponse : AbstratcResponse
    {
        public List<Transaction> data;

        public static TransactionsResponse FromResponse(IFlurlResponse response)
        {
            return new TransactionsResponse
            {
                status = response.StatusCode,
                message = StatusMessage.GetMessage(response.StatusCode),
                data = JsonConvert.DeserializeObject<List<Transaction>>(response.GetStringAsync().Result)
            };
        }
    }
}
