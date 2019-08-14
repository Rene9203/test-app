using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestApp.Application.Commands;
using TestApp.Application.Filter;
using TestApp.Application.ViewModels;
using TestApp.Core.Entities;
using TestApp.Core.Infraestructure;
using TestApp.Core.Service;
using TestApp.Core.SharedKernel;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    public class EmployerController : BaseController
    {
        private readonly OfferService _offerService;
        public EmployerController(IMediator mediator,
            IUserInSession userInSession,
            OfferService offerService) : base(mediator, userInSession)
        {
            _offerService = offerService;
        }

        [HttpGet("{id:Guid}/offers")]
        public async Task<IActionResult> Get([FromQuery]PageFilter pageFilter, [FromQuery]OfferViewModelFilter filter,
            [FromRoute]string id)
        {
           Expression<Func<Offer, bool>> offerFilter = OfferFilter.ConvertTo(filter);

            PagedFilter<Offer> pagedFilter =
                new PagedFilter<Offer>(pageFilter.Page, pageFilter.Limit, offerFilter);
            pagedFilter.Filter.AndAlso(x => x.EmployerId == id);

            Expression<Func<Offer, object>>[] includes = new Expression<Func<Offer, object>>[] {
                x => x.Employer,
                x => x.OfferType
            };

            PagedResult<Offer> offers = await _offerService.Get(pagedFilter, include: includes);
            IEnumerable<OfferViewModel> offerViewModel = OfferViewModel.From(offers.Data);

            PagedResult<OfferViewModel> queryResult = new PagedResult<OfferViewModel>(offerViewModel,
                offers.Page,
                offers.Limit,
                offers.ElementsCount);

            return Ok(queryResult);
        }

        [HttpPost("{id}/active/offer/{offerId}")]
        public async Task<IActionResult> AciveOffer([FromRoute]string id, [FromRoute]string offerId,
            [FromBody]UpdateActiveOfferCommand updateActiveOfferCommand)
        {
            updateActiveOfferCommand.EmployerId = id;
            updateActiveOfferCommand.OfferId = offerId;
            var result = await mediator.Send(updateActiveOfferCommand);

            if (!result)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Errors = new[] { "An error has occurred changing active state of Offer." }
                });

            return CreatedAtRoute("GetOfferById", new { id = offerId },
                new ApiResponse
                {
                    Success = true,
                    SuccessMessage = "The active state of Offer has been changed successfully."
                });
        }
    }
}
