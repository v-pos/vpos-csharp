using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vpos.Models
{
    public sealed class StatusMessage
    {
        public static readonly StatusMessage OK = new StatusMessage(200, "OK", "Payment or Refund request has been accepted and the location to monitor the status was provided through header location");
        public static readonly StatusMessage Accepted = new StatusMessage(202, "Accepted", "Refund request has been accepted and the location to monitor the status was provided through header location");
        public static readonly StatusMessage SeeOther = new StatusMessage(303, "See Other", "Payment or Refund request has finished processing and was relocated to the location provided through header location");
        public static readonly StatusMessage BadRequest = new StatusMessage(400, "Bad Request", "Your request is invalid");
        public static readonly StatusMessage Unauthorized = new StatusMessage(401,	"Unauthorized", "Your API key is wrong");
        public static readonly StatusMessage Forbidden = new StatusMessage(403,	"Forbidden", "The resource requested is hidden for administrators only");
        public static readonly StatusMessage NotFound = new StatusMessage(404,	"Not Found", "The specified resource could not be found");
        public static readonly StatusMessage MethodNotAllowed = new StatusMessage(405,	"Method Not Allowed", "You tried to access a resource with an invalid method");
        public static readonly StatusMessage NotAcceptable = new StatusMessage(406,	"Not Acceptable", "You requested a format that isn't json");
        public static readonly StatusMessage TooManyRequests = new StatusMessage(429,	"Too Many Requests", "You're requesting too much! Slow down");
        public static readonly StatusMessage InternalServerError = new StatusMessage(500,	"Internal Server Error", "We had a problem with our server.Try again later");
        public static readonly StatusMessage ServiceUnavailable = new StatusMessage(503,	"Service Unavailable", "We're temporarily offline for maintenance. Please try again later");

        public int Status { get; private set; }
        public string Meaning { get; private set; }
        public string Message { get; private set; }

        private StatusMessage(int status, string meaning, string message)
        {
            this.Status = status;
            this.Meaning = meaning;
            this.Message = message;
        }

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
