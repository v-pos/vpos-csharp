using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Models
{
    /// <summary>
    /// The main <c>Vpos</c> class Interacts with Vpos Api.
    /// Contains all methods to perform requests to the api
    /// <list type="bullet">
    /// <item>
    /// <term>NewPayment</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>NewRefund</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetTransaction</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetTransactions</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetRequest</term>
    /// <description></description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class has methods for creating payments, refunds and getting transactions</para>
    /// </remarks>
    public class LocationResponse : Response
    {
        /// <summary>
        /// The location from an http response
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Creates a instance of a location response
        /// </summary>
        /// <param name="status">An http status</param>
        /// <param name="message">The status message</param>
        /// <param name="location">The http header location</param>
        public LocationResponse(int status, string message, string location) 
            : base(status, message)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets the request ID from the location attribute.
        /// </summary>
        /// <returns>returns a string with the request id</returns>
        public string GetRequestID()
        {
            return Location.Replace("/api/v1/requests/", "").Replace("/api/v1/transactions/", "");
        }
    }
}
