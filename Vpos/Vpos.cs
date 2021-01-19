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
using Vpos.Utils;

namespace VposApi
{
    /// <summary>
    /// The main <c>Vpos</c> class Interacts with Vpos Api.
    /// Contains all methods to perform requests to the api
    /// <list type="bullet">
    /// <item>
    /// <term>NewPayment</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>NewRefund</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetTransaction</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetTransactions</term>
    /// <description></description>
    /// </item>
    /// <item>
    /// <term>GetRequest</term>
    /// <description></description>
    /// </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>This class has methods for creating payments, refunds and getting transactions</para>
    /// </remarks>
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
        /// </summary>
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

        /// <summary>
        /// Creates a new payment.
        /// Given a customer cellphone and the amount return a new payment.
        /// </summary>
        /// <returns>
        /// Returns <c>LocationResponse</c> object.
        /// </returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     var merchant = new Vpos();
        ///     LocationResponse location = merchant.NewPayment("900111222", "123.45");
        /// }
        /// catch(RequestFailedException ex)
        /// {
        ///     Console.log(ex.status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="RequestFailedException">Thrown when the status code is greather than or equal to 400</exception>
        /// <param name="customer"><c>customer</c> The customer mobile number</param>
        /// <param name="amount"><c>amount</c> The amount of money being charged</param>
        /// <param name="postID"><c>postID</c> the point of sale identification it defaults to the 'GPO_POS_ID' environment var</param>
        /// <param name="callbackUrl"><c>callbackUrl</c> the callback url it defaults to 'PAYMENT_CALLBACK_URL' environment var</param>
        public LocationResponse NewPayment(string customer, string amount, string postID = null, string callbackUrl = null)
        {
            IFlurlResponse result = HttpRequest("transactions")
                .PostJsonAsync(new {
                    type = "payment",
                    mobile = customer,
                    amount,
                    pos_id = postID ?? this.gpoPosID,
                    callback_url = callbackUrl ?? this.paymentCallbackUrl
                }).Result;

            int status = result.StatusCode;
            string message = StatusMessage.GetMessage(status);
            if (status == 202)
            {
                var locationResponse = new LocationResponse(status, message, Utils.GetLocation(result));
                return locationResponse;
            }
            else
                throw new RequestFailedException(status, message, Utils.GetDetails(result));
        }

        /// <summary> 
        /// Creates a new refund transaction, given the parent transaction id
        /// </summary>
        /// <returns>
        /// Returns <c>LocationResponse</c> object.
        /// </returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     var merchant = new Vpos();
        ///     LocationResponse location = merchant.NewRefund("900111222", "123.45");
        /// }
        /// catch(RequestFailedException ex)
        /// {
        ///     Console.log(ex.status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="RequestFailedException">Thrown when the status code is greather than or equal to 400</exception>
        /// <param name="parentTransactionId"><c>parentTransactionId</c> This is a string value of the transaction id you're requesting to be refunded.</param>
        /// <param name="supervisorCard"><c>supervisorCard</c> A 16 characters string digits representing the supervisor card provided by EMIS it defaults to GPO_SUPERVISOR_CARD environment var</param>
        /// <param name="callbackUrl"><c>callbackUrl</c> the callback url it defaults to 'PAYMENT_CALLBACK_URL' environment var</param>
        public LocationResponse NewRefund(string parentTransactionId, string supervisorCard = null, string callbackUrl = null)
        {
            IFlurlResponse result = HttpRequest("transactions")
                .PostJsonAsync(new
                {
                    type = "refund",
                    parent_transaction_id = parentTransactionId,
                    supervisor_card = supervisorCard ?? this.gpoSupervisorCard,
                    callback_url = callbackUrl ?? this.refundCallbackUrl
                }).Result;

            int status = result.StatusCode;
            string message = StatusMessage.GetMessage(status);
            if (status == 202)
            {
                var locationResponse = new LocationResponse(status, message, Utils.GetLocation(result));
                return locationResponse;
            }
            else
                throw new RequestFailedException(status, message, Utils.GetDetails(result));
        }

        /// <summary>
        /// Gets a single transaction.
        /// Given the transaction id or and error object if the transaction was not found.
        /// </summary>
        /// <returns>
        /// Returns <c>TransactionResponse</c> object.
        /// </returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     var merchant = new Vpos();
        ///     TransactionResponse transaction = merchant.GetTransaction("XCSd3csdbadshg67348tgyfr");
        /// }
        /// catch(RequestFailedException ex)
        /// {
        ///     Console.log(ex.status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="RequestFailedException">Thrown when the status code is greather than or equal to 400</exception>
        /// <param name="transactionId"><c>transactionId</c> The id of the transaction to retrieve</param>
        public TransactionResponse GetTransaction(string transactionId)
        {
            IFlurlResponse result = HttpRequest($"transactions/{transactionId}")
                .GetAsync().Result;

            int status = result.StatusCode;
            string message = StatusMessage.GetMessage(status);
            if (status == 200)
            {
                var transaction = result.GetJsonAsync<Transaction>().Result;
                var transactionResponse = new TransactionResponse(status, message, transaction);
                return transactionResponse;
            }
            else
                throw new RequestFailedException(status, message, Utils.GetDetails(result));
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns>
        /// Returns <c>TransactionsResponse</c> object.
        /// </returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     var merchant = new Vpos();
        ///     TransactionsResponse transactionsResponse = merchant.GetTransactions();
        /// }
        /// catch(RequestFailedException ex)
        /// {
        ///     Console.log(ex.status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="RequestFailedException">Thrown when the status code is greather than or equal to 400</exception>
        public TransactionsResponse GetTransactions()
        {
            IFlurlResponse result = HttpRequest("transactions")
                .GetAsync().Result;

            int status = result.StatusCode;
            string message = StatusMessage.GetMessage(status);
            if (status == 200)
            {
                var transactions = Utils.GetTransactions(result);
                var TransactionsResponse = new TransactionsResponse(status, message, transactions);
                return TransactionsResponse;
            }
            else
                throw new RequestFailedException(status, message, Utils.GetDetails(result));
        }

        /// <summary>
        /// retrieves a request
        /// Given its id
        /// </summary>
        /// <returns>
        /// Returns <c>RequestResponse</c> if the status is 200 or <c>LocationResponse</c> if the status is 303
        /// </returns>
        /// <example>
        /// <code>
        /// var merchant = new Vpos();
        /// try
        /// {
        ///     Response response = merchant.GetRequest("xSDRWFTs48unv9348ut9e");
        ///     if (response.status == 200)
        ///     {
        ///         RequestResponse requestResponse = (RequestResponse)response;
        ///     }
        ///     else if(response.status == 303)
        ///     {
        ///         LocationResponse locationResponse = (LocationResponse)response;
        ///     }
        /// }
        /// catch(RequestFailedException ex)
        /// {
        ///     Console.log(ex.status);
        /// }
        /// </code>
        /// </example>
        /// <exception cref="RequestFailedException">Thrown when the status code is greather than or equal to 400</exception>
        /// <param name="requestId"><c>requestId</c> The id of the request we are consulting</param>
        public Response GetRequest(string requestId)
        {
            IFlurlResponse result = HttpRequest($"requests/{requestId}")
                .GetAsync().Result;

            int status = result.StatusCode;
            string message = StatusMessage.GetMessage(status);
            if (status == 200)
            {
                var request = result.GetJsonAsync<Request>().Result;
                var requestResponse = new RequestResponse(status, message, request);
                return requestResponse;
            }
            else if(status == 303)
            {
                var locationResponse = new LocationResponse(status, message, Utils.GetLocation(result));
                return locationResponse;
            }
            else
                throw new RequestFailedException(status, message, Utils.GetDetails(result));
        }

        /// <summary>
        /// Creates a <c>IFlurlRequest</c> object
        /// with the base url and auth token.
        /// </summary>
        /// <returns>
        /// Returns <c>IFlurlRequest</c> object.
        /// </returns>
        /// <example>
        /// <code>
        /// string transactionID = "slndjntfv948tuy95e";
        /// IFlurlRequest request = HttpRequest($"transactions/{transactionID}");
        /// </code>
        /// </example>
        /// <param name="url"><c>url</c> the url to be called.</param>
        private IFlurlRequest HttpRequest(string url)
        {
            return this.host
                .WithOAuthBearerToken(this.merchantVposToken)
                .WithHeaders(new
                {
                    Content_Type = "application/json",
                    Idempotency_Key = Guid.NewGuid().ToString()
                })
                .WithAutoRedirect(false)
                .AllowAnyHttpStatus()
                .AppendPathSegment(url);
        }
    }
}
