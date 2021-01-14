using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom;
using System.Net.Http;
using vpos.Models;
using VposApi.Models;

namespace VposApi.Tests
{
    [TestClass]
    public class VposTests
    {
        private Vpos merchant;

        [TestInitialize]
        public void Init()
        {
            merchant = new Vpos();
        }

        [TestMethod]
        public void TestShouldCreateANewPaymentRequestTransaction()
        {
            LocationResponse response = (LocationResponse)merchant.NewPayment("992563019", "123.45");
            Assert.IsNotNull(response.location);
            Assert.AreEqual(response.status, 202);
        }

        [TestMethod]
        public void TestShouldNotCreateANewPaymentRequestTransactionIfCustomerFormatIsInvalid()
        {
            ApiErrorResponse response = (ApiErrorResponse)merchant.NewPayment("99256301", "123.45");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.status, 400);
            Assert.IsTrue(response.details.ContainsKey("mobile"));
        }
    
        [TestMethod]
        public void TestShouldNotCreateANewPaymentRequestTransactionIfAmountFormatIsInvalid()
        {
            ApiErrorResponse response = (ApiErrorResponse)merchant.NewPayment("992563019", "123.45.01");
            
            Assert.IsNotNull(response);
            Assert.AreEqual(response.status, 400);
            Assert.IsTrue(response.details.ContainsKey("amount"));
        }
    
        [TestMethod]
        public void TestShouldCreateANewRefundRequestTransaction()
        {
            LocationResponse response = (LocationResponse)merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv");
            Assert.IsNotNull(response.location);
            Assert.AreEqual(response.status, 202);
        }
    
        [TestMethod]
        public void TestShouldNotCreateANewRefundRequestTransactionIfParentTransactionIdIsNotPresent()
        {
            ApiErrorResponse response = (ApiErrorResponse)merchant.NewRefund(null);
            Assert.IsNotNull(response);
            Assert.AreEqual(response.status, 400);
            Assert.IsTrue(response.details.ContainsKey("parent_transaction_id"));
        }
    
        [TestMethod]
        public void TestShouldNotCreateANewRefundRequestTransactionIfSupervisorCardIsInvalid()
        {
            ApiErrorResponse response = (ApiErrorResponse)merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard: "");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.status, 400);
            Assert.IsTrue(response.details.ContainsKey("supervisor_card"));
        }
    
        [TestMethod]
        public void TestShouldGetAllTransactions()
        {
            TransactionsResponse response = (TransactionsResponse)merchant.GetTransactions();
            Assert.IsNotNull(response.data);
            Assert.AreEqual(response.status, 200);
        }
    
        [TestMethod]
        public void TestShouldGetASingleTransaction()
        {
            TransactionResponse response = (TransactionResponse)merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht");
            Assert.IsNotNull(response.data);
            Assert.AreEqual(response.status, 200);
        }
         
        [TestMethod]
        public void TestShouldNotGetANonExistentSingleTransaction()
        {
            ApiErrorResponse response = (ApiErrorResponse)merchant.GetTransaction("1jYQryG3Q");
            Assert.IsNotNull(response);
            Assert.AreEqual(response.status, 404);
        }
    }
}
