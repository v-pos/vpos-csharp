using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewPayment("992563019", "123.45"));
        }


        [TestMethod]
        public void TestShouldNotCreateANewPaymentRequestTransactionIfCustomerFormatIsInvalid()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewPayment("99256301", "123.45"));
        }

    
        [TestMethod]
        public void TestShouldNotCreateANewPaymentRequestTransactionIfAmountFormatIsInvalid()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewPayment("992563019", "123.45.01"));
        }


    
        [TestMethod]
        public void TestShouldCreateANewRefundRequestTransaction()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv"));
        }
          
    
        [TestMethod]
        public void TestShouldNotCreateANewRefundRequestTransactionIfParentTransactionIdIsNotPresent()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewRefund(null));
        }
      
    
        [TestMethod]
        public void TestShouldNotCreateANewRefundRequestTransactionIfSupervisorCardIsInvalid()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewRefund("1jYQryG3Qo4nzaOKgJxzWDs25Hv", supervisorCard:  "123123123123123"));
        }
    
        [TestMethod]
        public void TestShouldGetAllTransactions()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.GetTransactions());
        }
      
    
        [TestMethod]
        public void TestShouldGetASingleTransaction()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25Ht"));
        }
          
    
        [TestMethod]
        public void TestShouldNotGetANonExistentSingleTransaction()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.GetTransaction("1jYQryG3Qo4nzaOKgJxzWDs25H"));
        }
    
    
        [TestMethod]
        public void TestShouldGetARunningSingleRequestStatus()
        {
            Assert.ThrowsException<NotImplementedException>(() => merchant.NewPayment("925888553", "123.45"));

        }
    }
}
