using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    /// <summary>
    /// The class <c>RequestResponse</c> represents the response of a poll request call
    /// </summary>
    public class RequestResponse : Response
    {
        /// <summary>
        /// The poll request
        /// </summary>
        public Request Data { get; }

        /// <summary>
        /// Creates an instance of <c>RequestResponse</c> class
        /// </summary>
        /// <param name="status">the http status</param>
        /// <param name="message">the http status message</param>
        /// <param name="data">The request data</param>
        public RequestResponse(int status, string message, Request data) : base(status, message)
        {
            this.Data = data;
        }
    }
}
