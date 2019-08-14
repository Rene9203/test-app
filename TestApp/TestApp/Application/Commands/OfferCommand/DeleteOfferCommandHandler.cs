using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Application.Commands
{
    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, bool>
    {
        private readonly IOfferRepository _offerRepository;

        public DeleteOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            Offer offer = await _offerRepository.GetByIdAsync(request.Id);

            if (offer == null)
                throw new ApiException("The offer can't be found", StatusCodes.Status404NotFound);

            _offerRepository.Delete(offer);

            return await _offerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
