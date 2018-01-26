using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;

namespace InterestCalculator.Factory
{
    public class CreditCardInterestCalculatorFactory
    {
        public ICreditCardInterestCalculator GetInterestCalculator()
        {
            IRateFactory rateFactory = new RateFactory();
            return new CreditCardInterestCalculator(rateFactory);
        }
    }
}
