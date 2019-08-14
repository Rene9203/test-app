using MediatR;

namespace TestApp.Application.Commands
{
    public class DeleteOfferCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
