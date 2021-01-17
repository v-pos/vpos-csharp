using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vpos.Models;

namespace VposApi.Models
{
    public class Transaction
    {
        public string id;
        public string amount;
        public string mobile;
        public string parentTransactionId;
        public string type;
        public int posId;
        public string supervisorCard;
        public string clearingPeriod;
        public string status;
        public int statusReason;
        public DateTime statusDatetime;
    }
}
