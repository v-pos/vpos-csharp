using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VposApi
{
    /// <summary>
    /// Interacts with Vpos Api
    /// <summary>
    public class Vpos
    {
        private HttpClient httpClient;

        /// <summary>
        /// Initializes a Vpos Object
        public Vpos()
        {
            httpClient = new HttpClient();
        }

        public string NewPayment(string customer, string amount, string postID = null, string callbackUrl = null)
        {
            throw new NotImplementedException();
        }

        public string NewRefund(string parentTransactionId, string supervisorCard = null, string callbackUrl = null)
        {
            throw new NotImplementedException();
        }
        public string GetTransaction(string transactionId)
        {
            throw new NotImplementedException();
        }
        public string GetTransactions()
        {
            throw new NotImplementedException();
        }
        public string GetRequestId(string response)
        {
            throw new NotImplementedException();
        }
        public string GetRequest(string requestId)
        {
            throw new NotImplementedException();
        }
        private string ReturnVposObject(string response)
        {
            throw new NotImplementedException();
        }
        private string SetHeaders()
        {
            throw new NotImplementedException();
        }
        private string DefaultPosId()
        {
            throw new NotImplementedException();
        }
        private string DefaultSupervisorCard()
        {
            throw new NotImplementedException();
        }
        private string SetToken()
        {
            throw new NotImplementedException();
        }
        private string DefaultPaymentcallbackUrl()
        {
            throw new NotImplementedException();
        }
        private string Host()
        {
            throw new NotImplementedException();
        }

    }
}
