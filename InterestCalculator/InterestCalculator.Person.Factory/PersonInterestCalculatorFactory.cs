using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterestCalculator.Base;
using InterestCalculator;

namespace InterestCalculator.Factory
{
    public class PersonInterestCalculatorFactory
    {
        public IPersonInterestCalculator GetPersonInterestCalculator()
        {
            
            return new PersonInterestCalculator();
        }
    }
}
