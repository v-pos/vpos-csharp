using System;

namespace VposApi.Models
{
    /// <summary>
    /// The class <c>Request</c> represents a request in the poll
    /// </summary>
    public class Request
    {   
        /// <summary>
        /// the estimated time the request will be in the poll
        /// </summary>
        public float Eta { get; }
        /// <summary>
        /// The datetime the request was inserted in the poll
        /// </summary>
        public DateTime InsertedAt { get; }
    }
}
