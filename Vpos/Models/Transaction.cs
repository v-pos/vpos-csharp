using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    /// <summary>
    /// Represents a transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The transaction id
        /// </summary>
        public string id;
        /// <summary>
        /// The amount being charged
        /// </summary>
        public string amount;
        /// <summary>
        /// The user mobile number
        /// </summary>
        public string mobile;
        /// <summary>
        /// The parent transaction id
        /// </summary>
        public string parentTransactionId;
        /// <summary>
        /// The type of transction
        /// </summary>
        public string type;
        /// <summary>
        /// The point of cale id
        /// </summary>
        public int posId;
        /// <summary>
        /// The transaction supervisor card number
        /// </summary>
        public string supervisorCard;
        /// <summary>
        /// The transaction Clearing period
        /// </summary>
        public string clearingPeriod;
        /// <summary>
        /// The transaction status
        /// </summary>
        public string status;
        /// <summary>
        /// The transaction status reason
        /// </summary>
        public int statusReason;
        /// <summary>
        /// The status datetime
        /// </summary>
        public DateTime statusDatetime;
    }
}
