using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Models
{
    /// <summary>
    /// The <c>Response</c> class contains the base response structure.
    /// <list type="bullet">
    /// <item>
    /// <term>ContainsErrors</term>
    /// <description>Checks if object contains errors</description>
    /// </item>
    /// <item>
    /// <term>Is200x</term>
    /// <description>Checks if the error is 200, 201, 202,...,299</description>
    /// </item>
    /// <item>
    /// <term>Is300x</term>
    /// <description>Checks if the error is 300,301,...,399</description>
    /// </item>
    /// <item>
    /// <term>IsSuccessfull</term>
    /// <description>Checks if the error is 200x or 300x</description>
    /// </item>
    /// </list>
    /// </summary>
    public abstract class Response
    {
        /// <summary>
        /// The http status
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// The http sttaus message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Constructor for the class <c>Response</c>
        /// </summary>
        /// <param name="status">The http status</param>
        /// <param name="message">The http status message</param>
        protected Response(int status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        /// <summary>
        /// Checks if the object contains errors
        /// </summary>
        /// <returns>true if contains errors false otherwise</returns>
        public bool ContainsErrors()
        {
            bool containsErrors = Status >= 400;
            return containsErrors;
        }

        /// <summary>
        /// Checks if the http status 200-299
        /// </summary>
        /// <returns>true if the status between 200-299</returns>
        public bool Is2xx()
        {
            bool Is200x = Status >= 200 && Status <= 299;
            return Is200x;
        }

        /// <summary>
        /// Checks if the http status 300-399
        /// </summary>
        /// <returns>true if the status between 300-399</returns>
        public bool Is3xx()
        {
            bool Is300x = Status >= 300 && Status <= 399;
            return Is300x;
        }

        /// <summary>
        /// Checks if the Api calls was a success
        /// </summary>
        /// <returns>true if the api call was a success and false otherwise</returns>
        public bool IsSuccessfull()
        {
            bool IsSuccessfull = Is2xx() || Is3xx();
            return IsSuccessfull;
        }
    }
}
