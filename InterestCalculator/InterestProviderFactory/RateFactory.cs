using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;

namespace InterestCalculator.Factory
{
    public class RateFactory : IRateFactory
    {
        public double GetDefaultInterestRate(CreditCardType cardType)
        {
            switch(cardType)
            {
                case CreditCardType.Visa:
                    return 0.1;
                case CreditCardType.Master:
                    return 0.05;
                case CreditCardType.Discover:
                    return 0.01;
                default:
                    throw new Exception("Unknown credit card type");
            }
        }
    }
}
