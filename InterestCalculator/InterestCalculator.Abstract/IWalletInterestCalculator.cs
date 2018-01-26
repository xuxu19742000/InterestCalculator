using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestCalculator.Base
{
    public interface IWalletInterestCalculator
    {
        double GetInterest(Wallet wallet);
    }
}
