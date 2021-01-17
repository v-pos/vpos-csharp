using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using vpos.Models;
using VposApi.Models;
using vpos.Utils;

namespace VposApi
{
    /// <summary>
    /// Interacts with Vpos Api
    /// <summary>
    public class Vpos
    {
        private readonly string host;
        private readonly string gpoPosID;
        private readonly string gpoSupervisorCard;
        private readonly string merchantVposToken;
        private readonly string paymentCallbackUrl;
        private readonly string refundCallbackUrl;
        private readonly string vposEnvironment;

        /// <summary>
        /// Initializes a Vpos Object
        public Vpos()
        {
            this.gpoPosID = Environment.GetEnvironmentVariable("GPO_POS_ID");
            this.gpoSupervisorCard = Environment.GetEnvironmentVariable("GPO_SUPERVISOR_CARD");
            this.merchantVposToken = Environment.GetEnvironmentVariable("MERCHANT_VPOS_TOKEN");
            this.paymentCallbackUrl = Environment.GetEnvironmentVariable("PAYMENT_CALLBACK_URL");
            this.refundCallbackUrl = Environment.GetEnvironmentVariable("REFUND_CALLBACK_URL");
            this.vposEnvironment = Environment.GetEnvironmentVariable("VPOS_ENVIRONMENT");
            if (this.vposEnvironment == "PRD")
                this.host = "https://api.vpos.ao/api/v1";
            else
                this.host = "https://sandbox.vpos.ao/api/v1";
        }

        public AbstractResponse NewPayment(string customer, string amount, string postID = null, string callbackUrl = null)
        {
            IFlurlResponse result = PostHttpRequest("transactions")
                .PostJsonAsync(new {
                    type = "payment",
                    mobile = customer,
                    amount,
                    pos_id = postID ?? this.gpoPosID,
                    callback_url = callbackUrl ?? this.paymentCallbackUrl
                }).Result;

            int code = result.StatusCode;
            string message = StatusMessage.GetMessage(code);
            if (code == 202)
            {
                string location = result
                    .ResponseMessage
                    .Headers
                    .Location.ToString();
                return ResponseFactory.Create("LOCATION", code, message, location: location);
            }
            else
            {
                var details = Deserializers.ParseDetails(result.GetStringAsync().Result);
                return ResponseFactory.Create("ERROR", code, message, details: details);
            }
        }

        public AbstractResponse NewRefund(string parentTransactionId, string supervisorCard = null, string callbackUrl = null)
        {
            IFlurlResponse result = PostHttpRequest("transactions")
                .PostJsonAsync(new
                {
                    type = "refund",
                    parent_transaction_id = parentTransactionId,
                    supervisor_card = supervisorCard ?? this.gpoSupervisorCard,
                    callback_url = callbackUrl ?? this.refundCallbackUrl
                }).Result;

            int code = result.StatusCode;
            string message = StatusMessage.GetMessage(code);
            if (code == 202)
            {
                string location = result
                    .ResponseMessage
                    .Headers
                    .Location.ToString();
                return ResponseFactory.Create("LOCATION", code, message, location: location);
            }
            else
            {
                var details = Deserializers.ParseDetails(result.GetStringAsync().Result);
                return ResponseFactory.Create("ERROR", code, message, details: details);
            }
        }

        public AbstractResponse GetTransaction(string transactionId)
        {
            IFlurlResponse result = GetHttpRequest($"transactions/{transactionId}")
                .GetAsync().Result;

            int code = result.StatusCode;
            string message = StatusMessage.GetMessage(code);
            if (code == 200)
            {
                var transaction = result.GetJsonAsync<Transaction>().Result;
                return ResponseFactory.Create("TRANSACTION", code, message, transaction: transaction);
            }
            else
            {
                var details = Deserializers.ParseDetails(result.GetStringAsync().Result);
                return ResponseFactory.Create("ERROR", code, message, details: details);
            }
        }

        public AbstractResponse GetTransactions()
        {
            IFlurlResponse result = GetHttpRequest("transactions")
                .GetAsync().Result;

            int code = result.StatusCode;
            string message = StatusMessage.GetMessage(code);
            if (code == 200)
            {
                var transactions = Deserializers.ParseTransactions(result.GetStringAsync().Result);
                return ResponseFactory.Create("TRANSACTIONS_LIST", code, message, transactions: transactions);
            }
            else
            {
                var details = Deserializers.ParseDetails(result.GetStringAsync().Result);
                return ResponseFactory.Create("ERROR", code, message, details: details);
            }
        }

        public AbstractResponse GetRequest(string requestId)
        {
            IFlurlResponse result = GetHttpRequest($"requests/{requestId}")
                .GetAsync().Result;

            int code = result.StatusCode;
            string message = StatusMessage.GetMessage(code);
            if (code == 200)
            {

                var request = result.GetJsonAsync<Request>().Result;
                return ResponseFactory.Create("REQUEST", code, message, request: request);
            }
            else if(code == 303)
            {

                string location = result
                    .ResponseMessage
                    .Headers
                    .Location.ToString();
                return ResponseFactory.Create("LOCATION", code, message, location: location);
            }
            else
            {
                var details = Deserializers.ParseDetails(result.GetStringAsync().Result);
                return ResponseFactory.Create("ERROR", code, message, details: details);
            }
        }

        private IFlurlRequest GetHttpRequest(string url)
        {
            return this.host
                .WithOAuthBearerToken(this.merchantVposToken)
                .WithHeaders(new
                {
                    Idempotency_Key = Guid.NewGuid().ToString()
                })
                .AllowAnyHttpStatus()
                .AppendPathSegment(url);
        }

        private IFlurlRequest PostHttpRequest(string url)
        {
            return this.host
                .WithOAuthBearerToken(this.merchantVposToken)
                .WithHeaders(new
                {
                    Accept = "application/json",
                    Content_Type = "application/json",
                    Idempotency_Key = Guid.NewGuid().ToString()
                })
                .AllowAnyHttpStatus()
                .AppendPathSegment(url);
        }
    }
}
