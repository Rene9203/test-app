using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Application.Dto;
using TestApp.Application.ViewModels;
using TestApp.Core.Entities;
using TestApp.Core.Infraestructure;
using TestApp.Core.Service;
using TestApp.Core.SharedKernel;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    public class OfferTypeController : BaseController
    {
        private readonly OfferTypeService _offerTypeService;
        public OfferTypeController(IMediator mediator,
            IUserInSession userInSession,
            OfferTypeService offerTypeService) : base(mediator, userInSession)
        {
            _offerTypeService = offerTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]PageFilter pageFilter)
        {
            PagedFilter<OfferType> pagedFilter =
                new PagedFilter<OfferType>(pageFilter.Page, pageFilter.Limit);

            var offerTypes = await _offerTypeService.Get(pagedFilter);
            var offerTypesDto = OfferTypeDto.From(offerTypes.Data);

            PagedResult<OfferTypeDto> queryResult = new PagedResult<OfferTypeDto>(offerTypesDto,
                offerTypes.Page,
                offerTypes.Limit,
                offerTypes.ElementsCount);

            return Ok(queryResult);
        }
    }
}
