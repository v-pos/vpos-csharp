using System;
using System.Collections.Generic;

namespace vpos.Models
{
    /// <summary>
    /// The class <c>RequestFailedException</c> represents an error 4xx or 5xx during an API call
    /// </summary>
    public class RequestFailedException : Exception
    {
        /// <summary>
        /// The http status
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// The errors details
        /// </summary>
        public Dictionary<string, List<string>> Details { get; }

        /// <summary>
        /// Creates an instance of <c>RequestFailedException</c> with a status
        /// </summary>
        /// See <see cref="RequestFailedException(int, string)"/> for creating an exception with message.
        /// <param name="status">The http status</param>
        public RequestFailedException(int status) : this(status, null)
        {
        }

        /// <summary>
        /// Creates an instance of <c>RequestFailedException</c> with a status and message
        /// </summary>
        /// <param name="status">The http status</param>
        /// <param name="message">The http status message</param>
        public RequestFailedException(int status, string message) : this(status, message, null)
        {
        }

        /// <summary>
        /// Creates an instance of <c>RequestFailedException</c> with a status, message and errors details 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public RequestFailedException(int status, string message, Dictionary<string, List<string>> details) : base(message)
        {
            Status = status;
            Details = details;
        }
    }    
}
