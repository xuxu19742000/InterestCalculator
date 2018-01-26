using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using InterestCalculator.Base;

namespace InterestCalculator.Tests
{
    [TestFixture]
    public class WalletInterestCalculatorTest
    {
        private WalletInterestCalculator _calculator;
        private ICreditCardInterestCalculator _mockCreditCardInterestCalculator;
        private Wallet _wallet;

        [SetUp]
        public void init()
        {
            _mockCreditCardInterestCalculator = MockRepository.GenerateStub<ICreditCardInterestCalculator>();
            _calculator = new WalletInterestCalculator(_mockCreditCardInterestCalculator);
            _wallet = new Wallet();
        }

        [Test]
        public void InterestZeroForNullWallet()
        {
            _wallet = null;
            var result = _calculator.GetInterest(_wallet);

            _mockCreditCardInterestCalculator.AssertWasNotCalled(x=>x.GetInterest(Arg<CreditCard>.Is.Anything));
            Assert.AreEqual(0,result);
        }

        [Test]
        public void InterestZeroForWalletWithNullCreditCards()
        {
            var result = _calculator.GetInterest(_wallet);

            _mockCreditCardInterestCalculator.AssertWasNotCalled(x => x.GetInterest(Arg<CreditCard>.Is.Anything));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InterestZeroForWalletWithZeroCreditCards()
        {
            _wallet.Cards = new List<CreditCard>();

            var result = _calculator.GetInterest(_wallet);

            _mockCreditCardInterestCalculator.AssertWasNotCalled(x => x.GetInterest(Arg<CreditCard>.Is.Anything));
            Assert.AreEqual(0, result);
        }

        [Test]
        public void InterestWithOneCardCorrect()
        {
            var card = new CreditCard(CreditCardType.Visa);
            _wallet.Cards = new List<CreditCard> { card };
            _mockCreditCardInterestCalculator.Expect(x => x.GetInterest(Arg<CreditCard>.Is.Anything)).Return(10);

            var result = _calculator.GetInterest(_wallet);

            _mockCreditCardInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<CreditCard>.Is.Anything), x => x.Repeat.Once());
            Assert.AreEqual(10, result);
        }

        [Test]
        public void InterestWithTwoCardsCorrect()
        {
            var card1 = new CreditCard(CreditCardType.Visa);
            var card2 = new CreditCard(CreditCardType.Master);
            _wallet.Cards = new List<CreditCard> { card1, card2 };
            _mockCreditCardInterestCalculator.Expect(x => x.GetInterest(Arg<CreditCard>.Is.Equal(card1))).Return(10);
            _mockCreditCardInterestCalculator.Expect(x => x.GetInterest(Arg<CreditCard>.Is.Equal(card2))).Return(20);

            var result = _calculator.GetInterest(_wallet);

            _mockCreditCardInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<CreditCard>.Is.Equal(card1)), x => x.Repeat.Once());
            _mockCreditCardInterestCalculator.AssertWasCalled(x => x.GetInterest(Arg<CreditCard>.Is.Equal(card2)), x => x.Repeat.Once());
            Assert.AreEqual(30, result);
        }
    }
}
