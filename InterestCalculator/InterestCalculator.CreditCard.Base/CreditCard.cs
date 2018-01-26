using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestCalculator.Base
{
    public class CreditCard
    {        
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public CreditCardType CardType { get; private set; }

        public CreditCard(CreditCardType cardType)
        {
            CardType = cardType;
        }
    }
}
