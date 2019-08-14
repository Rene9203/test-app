using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Application.Commands
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, bool>
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateCommandHandler(IOfferRepository offerRepository, ICandidateRepository candidateRepository)
        {
            _offerRepository = offerRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Offer, object>>[] includes = new Expression<Func<Offer, object>>[] {
                x => x.Candidates
            };
            var offer = await _offerRepository.GetByIdAsync(request.OfferId, includes);
            if (offer == null)
                throw new ApiException("The offer can't be found.", StatusCodes.Status404NotFound);

            if (offer.Candidates.Any(x => x.Email == request.Email))
                throw new ApiException("Sorry you already apply on this offer", StatusCodes.Status400BadRequest);

            var candidate = new Candidate(request.Name, request.Email, request.OfferId);
            _candidateRepository.Add(candidate);

            return await _candidateRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
