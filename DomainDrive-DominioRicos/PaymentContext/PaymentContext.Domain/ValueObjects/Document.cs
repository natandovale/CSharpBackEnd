using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public EDocumentTypes Type { get; private set; }
        public string Number { get; private set; }

        public Document(string number, EDocumentTypes type)
        {
            Number = number;
            Type = type;
        }
    }
}
