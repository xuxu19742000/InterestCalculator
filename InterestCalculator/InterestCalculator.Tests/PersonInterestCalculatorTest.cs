using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator;
using Rhino.Mocks;
using InterestCalculator.Base;

namespace InterestCalculator.Tests
{
    [TestFixture]
    public class PersonInterestCalculatorTest
    {
        private PersonInterestCalculator _calculator;
        private IWalletInterestCalculator _mockWalletInterestCalculator;
        private Person _person;

        [SetUp]
        public void init()
        {
            _mockWalletInterestCalculator = MockRepository.GenerateStub<IWalletInterestCalculator>();
            _calculator = new PersonInterestCalculator(_mockWalletInterestCalculator);
            _person = new Person();
        }

        [Test]
        public void InterestZeroForNullPeson()
        {
            // Arrange
            _person = null;

            // Action
            var result = _calculator.GetInterest(_person);

            //Assert
            _mockWalletInterestCalculator.AssertWasNotCalled(x => x.GetInterest(Arg<Wallet>.Is.Anything));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InterestZeroForPersonWithNullWallet()
        {
            var result = _calculator.GetInterest(_person);

            _mockWalletInterestCalculator.AssertWasNotCalled(x => x.GetInterest(Arg<Wallet>.Is.Anything));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InterestZeroForPersonWithZeroWallet()
        {
            _person.Wallets = new List<Wallet>();

            var result = _calculator.GetInterest(_person);

            _mockWalletInterestCalculator.AssertWasNotCalled(x => x.GetInterest(Arg<Wallet>.Is.Anything));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InterestWithOneWalletCorrect()
        {
            // arrange
            var wallet = new Wallet();
            _person.Wallets = new List<Wallet> { wallet };
            _mockWalletInterestCalculator.Expect(x => x.GetInterest(Arg<Wallet>.Is.Anything)).Return(10);

            // action
            var result = _calculator.GetInterest(_person);

            // assert
            _mockWalletInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<Wallet>.Is.Anything), x=>x.Repeat.Once());
            Assert.AreEqual(10, result);
        }

        [Test]
        public void InterestWithTwoWalletsCorrect()
        {
            var wallet1 = new Wallet();
            var wallet2 = new Wallet();
            _person.Wallets = new List<Wallet> { wallet1, wallet2 };
            _mockWalletInterestCalculator.Expect(x => x.GetInterest(Arg<Wallet>.Is.Equal(wallet1))).Return(10);
            _mockWalletInterestCalculator.Expect(x => x.GetInterest(Arg<Wallet>.Is.Equal(wallet2))).Return(20);

            var result = _calculator.GetInterest(_person);

            _mockWalletInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<Wallet>.Is.Equal(wallet1)), x => x.Repeat.Once());
            _mockWalletInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<Wallet>.Is.Equal(wallet2)), x => x.Repeat.Once());
            Assert.AreEqual(30, result);
        }
    }
}
