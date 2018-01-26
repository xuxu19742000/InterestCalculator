using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;
using InterestCalculator.Factory;

namespace InterestCalculator
{
    public class WalletInterestCalculator : IWalletInterestCalculator
    {
        private ICreditCardInterestCalculator _calculator;

        public WalletInterestCalculator(ICreditCardInterestCalculator calculator)
        {
            _calculator = calculator;
        }

        public WalletInterestCalculator()
        {
            CreditCardInterestCalculatorFactory factory = new CreditCardInterestCalculatorFactory();
            _calculator = factory.GetInterestCalculator();
        }

        public double GetInterest(Wallet wallet)
        {
            if (wallet == null)
                return 0;
            if (wallet.Cards == null || !wallet.Cards.Any())
                return 0;

            return wallet.Cards.Select(x => _calculator.GetInterest(x)).Sum();
        }
    }
}
