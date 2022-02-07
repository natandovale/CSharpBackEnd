using System;

namespace PaymentContext.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        
        protected Entity(Guid id)
        {
            Id = Guid.NewGuid();
        }
    }
}
