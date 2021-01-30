
using System.Collections.Generic;
using vpos.Models;

namespace VposApi.Models
{
    /// <summary>
    /// Represents a response from an api call
    /// </summary>
    public class Response<T> : Response
    {
        /// <summary>
        /// Represents a data object returned by a n API call
        /// </summary>
        public T data;


        /// <summary>
        /// Creates an instance of <c>TransactionsResponse</c>
        /// </summary>
        /// <param name="status"> The http status</param>
        /// <param name="message">The status message</param>
        /// <param name="data">A list of transactions</param>
        /// <param name="details">A dictionary with the errors</param>
        /// <param name="location">The http header location</param>
        public Response(int status, string message, T data = default, Dictionary<string, List<string>> details = null, string location = null)
            : base(status, message, location, details) => this.data = data;
    }
}
