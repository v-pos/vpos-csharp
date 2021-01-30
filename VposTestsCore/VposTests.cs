using System;
using System.CodeDom;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;
using vpos.Models;
using VposApi.Models;
using Vpos.Utils;

namespace VposApi.TestCore
{
    public sealed class VposTests
    {
        [Fact(DisplayName = "NewPayment CustomerAndAmount 202")]
        [Trait("Payment", "Positive")]
        public void NewPayment_CustomerAndAmount_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            var response = merchant.NewPayment("992563019", "123.45");

            Assert.Equal(202, response.StatusCode);
        }

        [Fact(DisplayName = "NewPayment InvalidCustomer ReturnsStatus400")]
        public void NewPayment_InvalidCustomer_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_PHONE_NUMBER = "99256301";

            var response = merchant.NewPayment(INVALID_PHONE_NUMBER, "123.45");
            
            Assert.Equal(400, response.StatusCode);
        }

        [Fact(DisplayName = "NewPayment InvalidAmountFormat ReturnsStatus400")]
        public void NewPayment_InvalidAmountFormat_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_FORMAT_AMOUNT = "123.45.01";

            var response = merchant.NewPayment("992563019", INVALID_FORMAT_AMOUNT);

            Assert.Equal(400, response.StatusCode);
        }

        [Fact(DisplayName = "NewRefund ParentTransactionID ReturnsStatus202")]
        public void NewRefund_ParentTransactionID_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            var response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv");

            Assert.Equal(202, response.StatusCode);
        }

        [Fact(DisplayName = "NewRefund NullParentTransactionID ReturnsStatus400")]
        public void NewRefund_NullParentTransactionID_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();

            var response = merchant.NewRefund(null);

            Assert.Equal(400, response.StatusCode);
        }

        [Fact(DisplayName = "NewRefund InvalidSupervisorCard ReturnsStatus400")]
        public void NewRefund_InvalidSupervisorCard_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_SUPERVISOR_CARD = "";

            var response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard: INVALID_SUPERVISOR_CARD);

            Assert.Equal(400, response.StatusCode);
        }

        [Fact(DisplayName = "GetTransactions ReturnsStatus200")]
        public void GetTransactions_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            var response = merchant.GetTransactions();

            Assert.Equal(200, response.StatusCode);
        }

        [Fact(DisplayName = "GetTransaction ReturnsStatus200")]
        public void GetTransaction_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            Response response = merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht");

            Assert.Equal(200, response.StatusCode);
        }

        [Fact(DisplayName = "GetTransaction NonExistentTransactionID ReturnsStatus404")]
        public void GetTransaction_NonExistentTransactionID_ReturnsStatus404()
        {
            Vpos merchant = CreateDefaultVpos();
            const string TRANSACTION_ID = "1jYQryG3Q";

            var response = merchant.GetTransaction(TRANSACTION_ID);

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public void GetRequest_NewPayment_Returns200()
        {
            Vpos merchant = CreateDefaultVpos();

            var paymentResponse = merchant.NewPayment("992563019", "123.45");
            var requestId = Utils.GetRequestId(paymentResponse.Location);
            var response = merchant.GetRequest(requestId);

            Assert.Equal(200, response.StatusCode);
        }

        private Vpos CreateDefaultVpos()
        {
            return new Vpos();
        }
    }
}
