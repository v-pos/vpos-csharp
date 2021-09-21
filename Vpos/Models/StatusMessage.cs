using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VposModels.Models
{
    /// <summary>
    /// The class <c>StatusMessage</c> represents an http status.
    /// </summary>
    public sealed class StatusMessage
    {
        /// <summary>
        /// A static <c>StatusMessage</c> object for 200 status.
        /// </summary>
        public static readonly StatusMessage OK = new StatusMessage(200, "OK", "Payment or Refund request has been accepted and the location to monitor the status was provided through header location");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 202 status.
        /// </summary>
        public static readonly StatusMessage Accepted = new StatusMessage(202, "Accepted", "Refund request has been accepted and the location to monitor the status was provided through header location");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 303 status.
        /// </summary>
        public static readonly StatusMessage SeeOther = new StatusMessage(303, "See Other", "Payment or Refund request has finished processing and was relocated to the location provided through header location");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 400 status.
        /// </summary>
        public static readonly StatusMessage BadRequest = new StatusMessage(400, "Bad Request", "Your request is invalid");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 401 status.
        /// </summary>
        public static readonly StatusMessage Unauthorized = new StatusMessage(401,	"Unauthorized", "Your API key is wrong");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 403 status.
        /// </summary>
        public static readonly StatusMessage Forbidden = new StatusMessage(403,	"Forbidden", "The resource requested is hidden for administrators only");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 404 status.
        /// </summary>
        public static readonly StatusMessage NotFound = new StatusMessage(404,	"Not Found", "The specified resource could not be found");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 405 status.
        /// </summary>
        public static readonly StatusMessage MethodNotAllowed = new StatusMessage(405,	"Method Not Allowed", "You tried to access a resource with an invalid method");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 406 status.
        /// </summary>
        public static readonly StatusMessage NotAcceptable = new StatusMessage(406,	"Not Acceptable", "You requested a format that isn't json");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 429 status.
        /// </summary>
        public static readonly StatusMessage TooManyRequests = new StatusMessage(429,	"Too Many Requests", "You're requesting too much! Slow down");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 500 status.
        /// </summary>
        public static readonly StatusMessage InternalServerError = new StatusMessage(500,	"Internal Server Error", "We had a problem with our server.Try again later");
        /// <summary>
        /// A static <c>StatusMessage</c> object for 503 status.
        /// </summary>
        public static readonly StatusMessage ServiceUnavailable = new StatusMessage(503,	"Service Unavailable", "We're temporarily offline for maintenance. Please try again later");

        /// <summary>
        /// The http status
        /// </summary>
        public int Status { get; private set; }
        /// <summary>
        /// The status meaning
        /// </summary>
        public string Meaning { get; private set; }
        /// <summary>
        /// The status message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Creates an instance of Status Message
        /// </summary>
        /// <param name="status">The http status</param>
        /// <param name="meaning">The status meaning</param>
        /// <param name="message">The status message</param>
        private StatusMessage(int status, string meaning, string message)
        {
            this.Status = status;
            this.Meaning = meaning;
            this.Message = message;
        }

        /// <summary>
        /// Creates an instance of <c>StatusMessage</c> class given the status.
        /// </summary>
        /// <param name="status">The http status</param>
        /// <returns>Returns a <c>StatusMessage</c> object or null if <paramref name="status"/> is invalid</returns>
        public static StatusMessage GetStatus(int status)
        {
            switch (status)
            {
                case 200:
                    return StatusMessage.OK;
                case 202:
                    return StatusMessage.Accepted;
                case 303:
                    return StatusMessage.SeeOther;
                case 400:
                    return StatusMessage.BadRequest;
                case 401:
                    return StatusMessage.Unauthorized;
                case 403:
                    return StatusMessage.Forbidden;
                case 404:
                    return StatusMessage.NotFound;
                case 405:
                    return StatusMessage.MethodNotAllowed;
                case 406:
                    return StatusMessage.NotAcceptable;
                case 429:
                    return StatusMessage.TooManyRequests;
                case 500:
                    return StatusMessage.InternalServerError;
                case 503:
                    return StatusMessage.ServiceUnavailable;
                default: return null;
            }
        }

        /// <summary>
        /// Gets the message for a http status
        /// </summary>
        /// <param name="status">The http status</param>
        /// <returns>The status message or null if the stauts is invalid</returns>
        public static string GetMessage(int status)
        {
            StatusMessage statusMessage = GetStatus(status);
            if (statusMessage == null)
                return null;
            else
                return statusMessage.Message;
        }
    }
}
