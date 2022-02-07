using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardName { get; private set; }
        public string LastTransactionNumber { get; private set; }

        public CreditCardPayment(string cardHolderName, 
                                 string cardName, 
                                 string lastTransactionNumber,
                                 DateTime paidDate,
                                 DateTime expireDate,
                                 decimal total, 
                                 decimal totalPaid,
                                 string payer,
                                 Document document,
                                 Address address,
                                 Email email) : base(
                                     paidDate,
                                     expireDate,
                                     total,
                                     totalPaid,
                                     payer,
                                     document,
                                     address,
                                     email
                                     ) 
        {
            CardHolderName = cardHolderName;
            CardName = cardName;
            LastTransactionNumber = lastTransactionNumber;
        }

    }
}
