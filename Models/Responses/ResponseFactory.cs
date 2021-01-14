using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VposApi.Models;

namespace vpos.Models
{
    public class ResponseFactory
    {
        public static AbstratcResponse Create(string type,
                                              int status,
                                              string message,
                                              string location = null,
                                              Transaction transaction = null,
                                              List<Transaction> transactions = null,
                                              Request request = null,
                                              Dictionary<string, List<string>> details = null)
        {
            if (type == null)
                return null;
            else if (type.Equals("TRANSACTIONS_LIST"))
                return new TransactionsResponse
                {
                    status = status,
                    message = message,
                    data = transactions
                };
            else if (type.Equals("TRANSACTION"))
                return new TransactionResponse
                {
                    status = status,
                    message = message,
                    data = transaction
                };
            else if (type.Equals("REQUEST"))
                return new RequestResponse
                {
                    status = status,
                    message = message,
                    data = request
                };
            else if (type.Equals("LOCATION"))
                return new LocationResponse
                {
                    status = status,
                    message = message,
                    location = location
                };
            else if (type.Equals("ERROR"))
                return new ApiErrorResponse
                {
                    status = status,
                    message = message,
                    details = details
                };
            else
                return null;
        }
    }
}
