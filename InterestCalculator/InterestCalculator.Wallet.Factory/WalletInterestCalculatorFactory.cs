using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;

namespace InterestCalculator.Factory
{
    public class WalletInterestCalculatorFactory
    {
        public IWalletInterestCalculator GetWalletInterestCalculator()
        {
            return new WalletInterestCalculator();
        }
    }
}
