using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    /// <summary>
    /// The class <c>TransactionResponse</c> represents a transaction details response
    /// </summary>
    public class TransactionResponse : Response
    {
        /// <summary>
        /// The Transaction details
        /// </summary>
        public Transaction Data { get; }

        /// <summary>
        /// Creates an instance of TransactionResponse
        /// </summary>
        /// <param name="status">the http status</param>
        /// <param name="message">the status message</param>
        /// <param name="data">the transaction data</param>
        public TransactionResponse(int status, string message, Transaction data) : base(status, message)
        {
            this.Data = data;
        }
    }
}
