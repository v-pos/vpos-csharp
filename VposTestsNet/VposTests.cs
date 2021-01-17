using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Net.Http;
using Vpos.Models;

namespace Vpos.TestNet
{
    [TestClass]
    public class VposTests
    {
        [TestMethod]
        public void NewPayment_CustomerAndAmount_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.NewPayment("992563019", "123.45");

            Assert.AreEqual(202, response.status);
        }

        [TestMethod]
        public void NewPayment_InvalidCustomer_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_PHONE_NUMBER = "99256301";

            AbstractResponse response = merchant.NewPayment(INVALID_PHONE_NUMBER, "123.45");

            Assert.AreEqual(400, response.status);
        }

        [TestMethod]
        public void NewPayment_InvalidAmountFormat_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_FORMAT_AMOUNT = "123.45.01";

            AbstractResponse response = merchant.NewPayment("992563019", INVALID_FORMAT_AMOUNT);

            Assert.AreEqual(400, response.status);
        }

        [TestMethod]
        public void NewRefund_ParentTransactionID_ReturnsStatus202()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv");

            Assert.AreEqual(202, response.status);
        }

        [TestMethod]
        public void NewRefund_NullParentTransactionID_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.NewRefund(null);

            Assert.AreEqual(400, response.status);
        }

        [TestMethod]
        public void NewRefund_InvalidSupervisorCard_ReturnsStatus400()
        {
            Vpos merchant = CreateDefaultVpos();
            const string INVALID_SUPERVISOR_CARD = "";

            AbstractResponse response = merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard: INVALID_SUPERVISOR_CARD);

            Assert.AreEqual(400, response.status);
        }

        [TestMethod]
        public void GetTransactions_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransactions();

            Assert.AreEqual(200, response.status);
        }

        [TestMethod]
        public void GetTransaction_ReturnsStatus200()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht");

            Assert.AreEqual(200, response.status);
        }

        [TestMethod]
        public void GetTransaction_NonExistentTransactionID_ReturnsStatus404()
        {
            Vpos merchant = CreateDefaultVpos();

            AbstractResponse response = merchant.GetTransaction("1jYQryG3Q");

            Assert.AreEqual(404, response.status);
        }

        private Vpos CreateDefaultVpos()
        {
            return new Vpos();
        }
    }
}
