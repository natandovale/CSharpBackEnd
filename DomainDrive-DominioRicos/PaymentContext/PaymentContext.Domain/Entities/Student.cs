using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
            AddNotifications(name, document, email);
        }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in Subscriptions)
                if (sub.Active)
                    hasSubscriptionActive = true;

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voce já tem um assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Essa assinatura não possui pagamentos")
                );

            //if (hasSubscriptionActive)
            //    AddNotification("Student.Subscription", "Você já tem uma assinatura ativa");
        }
    }
}
