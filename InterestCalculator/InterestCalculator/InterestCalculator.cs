using InterestCalculator.Base;
using InterestCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Factory;

namespace InterestCalculator
{
    [TestFixture]
    public class InterestCalculator
    {
        private ICreditCardInterestCalculator _cardCalculator;
        private IWalletInterestCalculator _walletCalculator;
        private IPersonInterestCalculator _personCalculator;

        [SetUp]
        public void init()
        {
            _cardCalculator = new CreditCardInterestCalculatorFactory().GetInterestCalculator();
            _walletCalculator = new WalletInterestCalculatorFactory().GetWalletInterestCalculator();
            _personCalculator = new PersonInterestCalculatorFactory().GetPersonInterestCalculator();
        }

        [Test]
        public void OnePersonOneWalletThreeCards()
        {
            var card1 = new CreditCard(CreditCardType.Visa);
            var card2 = new CreditCard(CreditCardType.Master);
            var card3 = new CreditCard(CreditCardType.Discover);

            card1.Balance = 100;
            card2.Balance = 100;
            card3.Balance = 100;

            var wallet = new Wallet { Cards = new List<CreditCard> { card1, card2, card3 } };
            var person = new Person { Wallets = new List<Wallet> { wallet } };             

            var interest1 = _cardCalculator.GetInterest(card1);
            var interest2 = _cardCalculator.GetInterest(card2);
            var interest3 = _cardCalculator.GetInterest(card3);
           
            var personInterest = _personCalculator.GetInterest(person);

            Assert.AreEqual(10, interest1);
            Assert.AreEqual(5, interest2);
            Assert.AreEqual(1, interest3);

            Assert.AreEqual(16, personInterest);
        }

        [Test]
        public void OnePersonTwoWallets()
        {
            var card1 = new CreditCard(CreditCardType.Visa);
            var card2 = new CreditCard(CreditCardType.Master);
            var card3 = new CreditCard(CreditCardType.Discover);
            card1.Balance = 100;
            card2.Balance = 100;
            card3.Balance = 100;

            var wallet1 = new Wallet { Cards = new List<CreditCard> { card1, card3 } };
            var wallet2 = new Wallet { Cards = new List<CreditCard> { card2 } };
            var person = new Person { Wallets = new List<Wallet> { wallet1, wallet2 } };

            var wInterest1 = _walletCalculator.GetInterest(wallet1);
            var wInterest2 = _walletCalculator.GetInterest(wallet2);
            var pInterest = _personCalculator.GetInterest(person);

            Assert.AreEqual(11, wInterest1);
            Assert.AreEqual(5, wInterest2);
            Assert.AreEqual(16, pInterest);
        }

        [Test]
        public void TwoPerson()
        {
            var card1 = new CreditCard(CreditCardType.Visa);
            var card2 = new CreditCard(CreditCardType.Master);
            var card3 = new CreditCard(CreditCardType.Visa);
            var card4 = new CreditCard(CreditCardType.Master);
            card1.Balance = 100;
            card2.Balance = 100;
            card3.Balance = 100;
            card4.Balance = 100;

            var wallet1 = new Wallet { Cards = new List<CreditCard> { card1, card2 } };
            var wallet2 = new Wallet { Cards = new List<CreditCard> { card3, card4 } };

            var person1 = new Person { Wallets = new List<Wallet> { wallet1 } };
            var person2 = new Person { Wallets = new List<Wallet> { wallet2 } };

            var wInterest1 = _walletCalculator.GetInterest(wallet1);
            var wInterest2 = _walletCalculator.GetInterest(wallet2);
            var pInterest1 = _personCalculator.GetInterest(person1);
            var pInterest2 = _personCalculator.GetInterest(person2);

            Assert.AreEqual(15, wInterest1);
            Assert.AreEqual(15, wInterest2);
            Assert.AreEqual(15, pInterest1);
            Assert.AreEqual(15, pInterest2);
        }
    }
}
