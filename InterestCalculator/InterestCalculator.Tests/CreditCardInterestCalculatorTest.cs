using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator;
using InterestCalculator.Base;
using Rhino.Mocks;

namespace InterestCalculator.Tests
{
    [TestFixture]
    public class CreditCardInterestCalculatorTest
    {
        private CreditCardInterestCalculator _calculator;
        private IRateFactory _mockRateFactory;

        [SetUp]
        public void init()
        {
            _mockRateFactory = MockRepository.GenerateStub<IRateFactory>();
            _calculator = new CreditCardInterestCalculator(_mockRateFactory);
        }

        [Test]
        public void ZeroInterestForNullCard()
        {
            CreditCard card = null;

            var result = _calculator.GetInterest(card);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void VisaCardInterestCorrect()
        {
            CreditCard card = new CreditCard(CreditCardType.Visa);
            card.Balance = 100;
            _mockRateFactory.Expect(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Visa))).Return(0.1);

            var result = _calculator.GetInterest(card);

            Assert.AreEqual(10, result);
            _mockRateFactory.AssertWasCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Visa)), x => x.Repeat.Once());
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Master)));
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Discover)));

        }

        [Test]
        public void MasterCardInterestCorrect()
        {
            CreditCard card = new CreditCard(CreditCardType.Master);
            card.Balance = 100;
            _mockRateFactory.Expect(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Master))).Return(0.05);

            var result = _calculator.GetInterest(card);

            Assert.AreEqual(5, result);
            _mockRateFactory.AssertWasCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Master)), x => x.Repeat.Once());
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Visa)));
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Discover)));

        }

        [Test]
        public void DiscoverCardInterestCorrect()
        {
            CreditCard card = new CreditCard(CreditCardType.Discover);
            card.Balance = 100;
            _mockRateFactory.Expect(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Discover))).Return(0.01);

            var result = _calculator.GetInterest(card);

            Assert.AreEqual(1, result);
            _mockRateFactory.AssertWasCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Discover)), x => x.Repeat.Once());
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Master)));
            _mockRateFactory.AssertWasNotCalled(x => x.GetDefaultInterestRate(Arg<CreditCardType>.Is.Equal(CreditCardType.Visa)));

        }
    }
}
