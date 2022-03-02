using Flunt.Validations;
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

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(), "Document.Nubem", "Documento invalido")
                );
        }

        private bool Validate()
        {
            if (Type == EDocumentTypes.CNPJ && Number.Length == 14) return true;
            if (Type == EDocumentTypes.CPF && Number.Length == 11) return true;
            return false;
        }

    }
}
