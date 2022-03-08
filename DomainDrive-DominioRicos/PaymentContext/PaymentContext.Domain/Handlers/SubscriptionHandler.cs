using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
