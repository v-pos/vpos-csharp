using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Utils
{
    class Deserializers
    {
        public static Dictionary<string, List<string>> ParseDetails(string detailsString)
        {
            Dictionary<string, List<string>> details = new Dictionary<string, List<string>>();
            try
            {
                JObject json = JObject.Parse(detailsString);
                if (json.ContainsKey("errors"))
                {
                    return json["errors"].AsJEnumerable()
                        .Cast<JProperty>()
                        .ToDictionary(
                            p => p.Name,
                            p => p.Value.Children().Values<string>().ToList()
                        );
                }
                return details;
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }

        public static List<Transaction> ParseTransactions(string transactionsString)
        {
            return JsonConvert.DeserializeObject<List<Transaction>>(transactionsString);
        }
    }
}
