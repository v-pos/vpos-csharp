using System;
using System.CodeDom;
using System.Net.Http;
using Xunit;
using Xunit.Sdk;
using vpos.Models;
using VposApi.Models;

namespace VposApi.TestCore
{
    public sealed class VposTests
    {
        [Fact]
        public void NewPayment_CustomerAndAmount_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            LocationResponse response = merchant.NewPayment("992563019", "123.45");

            Assert.Equal(202, response.Status);
        }

        [Fact]
        public void NewPayment_InvalidCustomer_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_PHONE_NUMBER = "99256301";

            Action action = () => merchant.NewPayment(INVALID_PHONE_NUMBER, "123.45");

            RequestFailedException err = Assert.Throws<RequestFailedException>(action);
            
            Assert.Equal(400, err.Status);
        }

        [Fact]
        public void NewPayment_InvalidAmountFormat_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_FORMAT_AMOUNT = "123.45.01";

            Action action = () => merchant.NewPayment("992563019", INVALID_FORMAT_AMOUNT);

            RequestFailedException err = Assert.Throws<RequestFailedException>(action);
            Assert.Equal(400, err.Status);
        }

        [Fact]
        public void NewRefund_ParentTransactionID_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            LocationResponse response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv");

            Assert.Equal(202, response.Status);
        }

        [Fact]
        public void NewRefund_NullParentTransactionID_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();

            Action action = () => merchant.NewRefund(null);

            RequestFailedException err = Assert.Throws<RequestFailedException>(action);
            Assert.Equal(400, err.Status);
        }

        [Fact]
        public void NewRefund_InvalidSupervisorCard_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_SUPERVISOR_CARD = "";

            Action action = () => merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard: INVALID_SUPERVISOR_CARD);

            RequestFailedException err = Assert.Throws<RequestFailedException>(action);
            Assert.Equal(400, err.Status);
        }

        [Fact]
        public void GetTransactions_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            TransactionsResponse response = merchant.GetTransactions();

            Assert.Equal(200, response.Status);
        }

        [Fact]
        public void GetTransaction_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            TransactionResponse response = merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht");

            Assert.Equal(200, response.Status);
        }

        [Fact]
        public void GetTransaction_NonExistentTransactionID_ReturnsStatus404()
        {
            Vpos merchant = CreateDefaultVpos();
            const string TRANSACTION_ID = "1jYQryG3Q";

            Action action = () => merchant.GetTransaction(TRANSACTION_ID);

            RequestFailedException err = Assert.Throws<RequestFailedException>(action);
            Assert.Equal(404, err.Status);
        }

        private Vpos CreateDefaultVpos()
        {
            return new Vpos();
        }
    }
}
