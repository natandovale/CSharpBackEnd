using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
    }
}
