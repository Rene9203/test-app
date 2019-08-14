using System.Collections.Generic;
using System.Linq;
using TestApp.Application.Dto;
using TestApp.Core.Entities;

namespace TestApp.Application.ViewModels
{
    public class OfferViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public string OfferTypeName { get; set; }

        public EmployerDto Employer { get; set; }

        public OfferTypeDto OfferType { get; set; }

        public static OfferViewModel From(Offer offer)
        {
            return new OfferViewModel()
            {
                Id = offer.Id,
                Description = offer.Description,
                Employer = EmployerDto.From(offer.Employer),
                OfferType = OfferTypeDto.From(offer.OfferType),
                Active = offer.Active,
                OfferTypeName = offer.OfferType.Name
            };
        }

        public static IEnumerable<OfferViewModel> From(IEnumerable<Offer> offers) => offers.Select(From);
    }
}