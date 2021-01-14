using Flurl.Http;
using Flurl.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vpos.Models
{
    public class ApiErrorResponse : AbstratcResponse
    {
        public Dictionary<string, List<string>> details;
        
        public static ApiErrorResponse FromResponse(IFlurlResponse response)
        {
            return new ApiErrorResponse
            {
                status = response.StatusCode,
                message = StatusMessage.GetMessage(response.StatusCode),
                details = ParseDetails(response.GetStringAsync().Result)
            };
        }

        private static Dictionary<string, List<string>> ParseDetails(string detailsString)
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
            catch(JsonReaderException ex)
            {
                return new Dictionary<string, List<string>>();
            }
        }
    }

    
}
