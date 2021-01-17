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

            AbstractResponse response = merchant.NewPayment("992563019", "123.45");

            Assert.Equal(202, response.status);
        }

        [Fact]
        public void NewPayment_InvalidCustomer_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_PHONE_NUMBER = "99256301";

            AbstractResponse response = merchant.NewPayment(INVALID_PHONE_NUMBER, "123.45");

            Assert.Equal(400, response.status);
        }

        [Fact]
        public void NewPayment_InvalidAmountFormat_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_FORMAT_AMOUNT = "123.45.01";

            AbstractResponse response = merchant.NewPayment("992563019", INVALID_FORMAT_AMOUNT);

            Assert.Equal(400, response.status);
        }

        [Fact]
        public void NewRefund_ParentTransactionID_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv");

            Assert.Equal(202, response.status);
        }

        [Fact]
        public void NewRefund_NullParentTransactionID_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.NewRefund(null);

            Assert.Equal(400, response.status);
        }

        [Fact]
        public void NewRefund_InvalidSupervisorCard_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_SUPERVISOR_CARD = "";

            AbstractResponse response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard: INVALID_SUPERVISOR_CARD);

            Assert.Equal(400, response.status);
        }

        [Fact]
        public void GetTransactions_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransactions();

            Assert.Equal(200, response.status);
        }

        [Fact]
        public void GetTransaction_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht");

            Assert.Equal(200, response.status);
        }

        [Fact]
        public void GetTransaction_NonExistentTransactionID_ReturnsStatus404()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransaction("1jYQryG3Q");

            Assert.Equal(404, response.status);
        }

        private Vpos CreateDefaultVpos()
        {
            return new Vpos();
        }
    }
}
