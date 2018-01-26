using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestCalculator.Base
{
    public interface IRateFactory
    {
        double GetDefaultInterestRate(CreditCardType cardType);
    }
}
