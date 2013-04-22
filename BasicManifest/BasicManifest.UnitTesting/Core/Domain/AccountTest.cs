using BasicManifest.Core.Domain;
using NUnit.Framework;

namespace BasicManifest.UnitTesting.Core.Domain
{
    [TestFixture]
    public class AccountTest
    {
        private Account _account;

        [SetUp]
        public void Setup()
        {
            _account = new Account();
        }

        [Test]
        public void Balance_IsEqualToSingleTransaction()
        {
            var transaction = new Transaction(5);
            _account.Add(transaction);
            Assert.That(_account.Balance, Is.EqualTo(5));
        }

        [Test]
        public void Balance_IsEqualToSumOfTransactions()
        {
            _account.Add(new Transaction(5));
            _account.Add(new Transaction(-10));
            Assert.That(_account.Balance, Is.EqualTo(-5));
        }
    }
}