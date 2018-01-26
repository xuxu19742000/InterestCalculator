using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;
using InterestCalculator.Factory;

namespace InterestCalculator
{
    public class CreditCardInterestCalculator : ICreditCardInterestCalculator
    {
        private IRateFactory _rateProvider;

        public CreditCardInterestCalculator(IRateFactory rateProvider)
        {
            _rateProvider = rateProvider;
        }

        public CreditCardInterestCalculator()
        {
            _rateProvider = new RateFactory();
        }

        public double GetInterest(Base.CreditCard card)
        {
            if (card == null)
                return 0;

            var rate = _rateProvider.GetDefaultInterestRate(card.CardType);
            var interest = card.Balance * rate;

            Console.Out.WriteLine(string.Format("Card Type: {0}, interest: {1}", card.CardType, interest));
            return interest;
        }
    }
}
