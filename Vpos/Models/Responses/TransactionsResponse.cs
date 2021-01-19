using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    /// <summary>
    /// Represents a response from a transactions list request
    /// </summary>
    public class TransactionsResponse : Response
    {
        /// <summary>
        /// Represents a list of transactions
        /// </summary>
        public List<Transaction> data;

        /// <summary>
        /// Creates an instance of <c>TransactionsResponse</c>
        /// </summary>
        /// <param name="status"> The http status</param>
        /// <param name="message">The status message</param>
        /// <param name="data">A list of transactions</param>
        public TransactionsResponse(int status, string message, List<Transaction> data) : base(status, message)
        {
            this.data = data;
        }
    }
}
