using System.Collections.Generic;

namespace VposModels.Models
{
    /// <summary>
    /// The <c>Response</c> class contains the base response structure.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The http status
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// The http sttaus message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The location http location attribute
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Errors thrown by the api call
        /// </summary>
        public Dictionary<string, List<string>> Details { get; }

        /// <summary>
        /// Constructor for the class <c>Response</c>
        /// </summary>
        /// <param name="status">The http status</param>
        /// <param name="message">The http status message</param>
        protected Response(int status, string message)
        {
            StatusCode = status;
            Message = message;
        }

        /// <summary>
        /// Constructor for the class <c>Response</c> for redirect operation
        /// </summary>
        /// <param name="status">The http status</param>
        /// <param name="message">The http status message</param>
        /// <param name="location">the http headers location attribute</param>
        /// <param name="details">Errors messages</param>
        public Response(int status, string message, string location = null, Dictionary<string, List<string>> details = null) :  this(status, message)
        {
            this.Location = location;
            this.Details = details;
        }
    }
}
