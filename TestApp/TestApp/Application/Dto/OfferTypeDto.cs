using System.Collections.Generic;
using System.Linq;
using TestApp.Core.Entities;

namespace TestApp.Application.Dto
{
    public class OfferTypeDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public static OfferTypeDto From(OfferType offerType)
        {
            return new OfferTypeDto()
            {
                Id = offerType.Id,
                Name = offerType.Name
            };
        }

        public static IEnumerable<OfferTypeDto> From(IEnumerable<OfferType> offerTypes) => offerTypes.Select(From);
    }
}
