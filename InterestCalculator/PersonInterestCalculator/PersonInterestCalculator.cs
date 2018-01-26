using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;
using InterestCalculator.Factory;

namespace InterestCalculator
{
    public class PersonInterestCalculator : IPersonInterestCalculator
    {
        private IWalletInterestCalculator _calculator;
        
        public PersonInterestCalculator(IWalletInterestCalculator calculator)
        {
            _calculator = calculator;
        }

        public PersonInterestCalculator()
        {
            WalletInterestCalculatorFactory factory = new WalletInterestCalculatorFactory();
            _calculator = factory.GetWalletInterestCalculator();
        }

        public double GetInterest(Base.Person person)
        {
            if (person == null)
                return 0;

            if (person.Wallets == null || !person.Wallets.Any())
                return 0;

            return person.Wallets.Select(x => _calculator.GetInterest(x)).Sum();
        }
    }
}
