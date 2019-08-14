using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Application.Commands
{
    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, bool>
    {
        private readonly IOfferRepository _offerRepository;

        public UpdateOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<bool> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            Offer offer = await _offerRepository.GetByIdAsync(request.Id);

            if (offer == null)
                throw new ApiException("The offer can't be found", StatusCodes.Status404NotFound);

            offer.Update(request.Description, request.OfferTypeId);
            _offerRepository.Update(offer);

            return await _offerRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
