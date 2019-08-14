using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Core.SharedKernel.Service;

namespace TestApp.Core.Service
{
    public class OfferTypeService : GenericService<OfferType, string>
    {
        public OfferTypeService(IOfferTypeRepository repository) : base(repository)
        {
        }
    }
}
