using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VposApi.Models;

namespace VposUtilities.Utils
{
    /// <summary>
    /// A class with utilities functions
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Extracts the location header from a http response
        /// </summary>
        /// <param name="response">A http response object</param>
        /// <returns>Return a location string</returns>
        public static string GetLocation(this IFlurlResponse response)
        {
            return response.ResponseMessage
                    .Headers
                    .Location.ToString();
        }

        /// <summary>
        /// Extracts the errors from a http response
        /// </summary>
        /// <param name="response">A http response object</param>
        /// <returns>Returns a dictionary with the api call errors</returns>
        public static Dictionary<string, List<string>> GetDetails(this IFlurlResponse response)
        {
            Dictionary<string, List<string>> details = new Dictionary<string, List<string>>();
            try
            {
                JObject json = JObject.Parse(response.GetStringAsync().Result);
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

        /// <summary>
        /// gets request id from a location string
        /// </summary>
        /// <param name="location">An http header location</param>
        /// <returns>requestid or transaction id</returns>
        public static string GetRequestId(string location)
        {
            string requestId = location.Replace("/api/v1/requests/", "").Replace("/api/v1/transactions/", "");
            return requestId;
        }
    }
}
