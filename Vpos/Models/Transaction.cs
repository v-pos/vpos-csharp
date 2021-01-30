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
        public string Id { get; }

        /// <summary>
        /// The amount being charged
        /// </summary>
        public string Amount { get; }

        /// <summary>
        /// The user mobile number
        /// </summary>
        public string Mobile {get;}

        /// <summary>
        /// The parent transaction id
        /// </summary>
        public string ParentTransactionId {get;}

        /// <summary>
        /// The type of transaction
        /// </summary>
        public string Type {get;}

        /// <summary>
        /// The point of cale id
        /// </summary>
        public int PosId {get;}

        /// <summary>
        /// The transaction supervisor card number
        /// </summary>
        public string SupervisorCard {get;}

        /// <summary>
        /// The transaction Clearing period
        /// </summary>
        public string ClearingPeriod {get;}

        /// <summary>
        /// The transaction status
        /// </summary>
        public string Status {get;}

        /// <summary>
        /// The transaction status reason
        /// </summary>
        public int? StatusReason {get;}

        /// <summary>
        /// The status datetime
        /// </summary>
        public string StatusDatetime {get;}

        /// <summary>
        /// Contructs a transaction object
        /// </summary>
        /// <param name="id">The transaction id</param>
        /// <param name="amount">The amount being charged</param>
        /// <param name="mobile">The user mobile number</param>
        /// <param name="parentTransactionId">The parent transaction id</param>
        /// <param name="type">The type of transaction</param>
        /// <param name="posId">The point of cale id</param>
        /// <param name="supervisorCard">The transaction supervisor card number</param>
        /// <param name="clearingPeriod">The transaction Clearing period</param>
        /// <param name="status">The transaction status</param>
        /// <param name="statusReason">The transaction status reason</param>
        /// <param name="statusDatetime">The status datetime</param>
        public Transaction(string id, string amount,
            string mobile, string parentTransactionId, 
            string type, int posId, string supervisorCard,
            string clearingPeriod, string status,
            int? statusReason, string statusDatetime)
        {
            Id = id;
            Amount = amount;
            Mobile = mobile;
            ParentTransactionId = parentTransactionId;
            Type = type;
            PosId = posId;
            SupervisorCard = supervisorCard;
            ClearingPeriod = clearingPeriod;
            Status = status;
            StatusReason = statusReason;
            StatusDatetime = statusDatetime;
        }
    }
}
