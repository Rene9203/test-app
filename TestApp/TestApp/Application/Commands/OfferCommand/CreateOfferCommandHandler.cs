using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Application.Commands
{
    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, Offer>
    {
        private readonly IOfferRepository _offerReposiyoty;

        public CreateOfferCommandHandler(IOfferRepository offerReposiyoty)
        {
            _offerReposiyoty = offerReposiyoty;
        }

        public async Task<Offer> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = new Offer(request.Description, request.OfferTypeId, request.EmployerId);

            _offerReposiyoty.Add(offer);

            return await _offerReposiyoty.UnitOfWork.SaveEntitiesAsync(cancellationToken) ? offer : null;
        }
    }
}
