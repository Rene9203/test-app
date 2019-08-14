using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Application.Commands.OfferCommand
{
    public class UpdateActiveOfferCommandHandler : IRequestHandler<UpdateActiveOfferCommand, bool>
    {
        private readonly IOfferRepository _offerRepository;

        public UpdateActiveOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(UpdateActiveOfferCommand request, CancellationToken cancellationToken)
        {
            if(request.Active == true)
            {
                var offersActiveCount = await _offerRepository.GetOfferActiveCountAsync(request.EmployerId);
                if (offersActiveCount >= 10)
                    throw new ApiException("Sorry already have 10 offer active, please desactive one", StatusCodes.Status400BadRequest);
            }
            var offer = await _offerRepository.GetByIdAsync(request.OfferId);
            offer.SetActive(request.Active);
            _offerRepository.Update(offer);

            return await _offerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
