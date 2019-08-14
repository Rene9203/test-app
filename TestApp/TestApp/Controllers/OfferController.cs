using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApp.Application.Commands;
using TestApp.Application.Exceptions;
using TestApp.Application.Filter;
using TestApp.Application.ViewModels;
using TestApp.Core.Entities;
using TestApp.Core.Infraestructure;
using TestApp.Core.Service;
using TestApp.Core.SharedKernel;
using TestApp.Identity;
using TestApp.Infraestructure;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    public class OfferController : BaseController
    {
        private readonly OfferService _offerService;
        public OfferController(IMediator mediator,
            IUserInSession userInSession,
            OfferService offerService) : base(mediator, userInSession)
        {
            _offerService = offerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] PageFilter pageFilter, [FromQuery] OfferViewModelFilter filter)
        {
            Expression<Func<Offer, bool>> offerFilter = OfferFilter.ConvertTo(filter);
            offerFilter = offerFilter.AndAlso(x => x.Active == true);

            PagedFilter<Offer> pagedFilter =
                new PagedFilter<Offer>(pageFilter.Page, pageFilter.Limit, offerFilter);

            Expression<Func<Offer, object>>[] includes = new Expression<Func<Offer, object>>[] {
                x => x.Employer,
                x => x.OfferType
            };


            PagedResult<Offer> offers = await _offerService.Get(pagedFilter,include: includes);
            IEnumerable<OfferViewModel> offerViewModel = OfferViewModel.From(offers.Data);

            PagedResult<OfferViewModel> queryResult = new PagedResult<OfferViewModel>(offerViewModel,
                offers.Page,
                offers.Limit,
                offers.ElementsCount);

            return Ok(queryResult);
        }

        [HttpGet("{id}", Name = "GetOfferById")]
        public async Task<IActionResult> GetOfferById([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ApiException("The Offer can't be found.", StatusCodes.Status404NotFound);

            Offer offer = await _offerService.GetByIdAsync(id);
            if (offer == null)
                throw new ApiException("The Offer can't be found.", StatusCodes.Status404NotFound);

            OfferViewModel offerViewModel = OfferViewModel.From(offer);
            return Ok(SimpleResponse<OfferViewModel>.Create(offerViewModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody] CreateOfferCommand createOfferCommand)
        {
            Offer offer = await mediator.Send(createOfferCommand);

            if (offer == null)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Errors = new[] { "An error has occurred creating the Offer." }
                });

            return CreatedAtRoute("GetOfferById", new { id = offer.Id },
                new ApiResponse
                {
                    Success = true,
                    SuccessMessage = "Offer created successfully."
                });
        }

        [HttpPost("{id}/apply")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Apply([FromBody] CreateCandidateCommand createCandidateCommand,
            [FromRoute]string id)
        {
            createCandidateCommand.OfferId = id;
            var candidate = await mediator.Send(createCandidateCommand);

            if (!candidate)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Errors = new[] { "An error occurr when try apply." }
                });

            return CreatedAtRoute("GetOfferById", new { id },
                new ApiResponse
                {
                    Success = true,
                    SuccessMessage = "Thanks for apply for this offer."
                });
        }

        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> Put([FromBody] UpdateOfferCommand updateOfferCommand)
        {
            bool result = await mediator.Send(updateOfferCommand);

            return result ? StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Success = true,
                SuccessMessage = "Offer updated successfully."
            }) : BadRequest(new ApiResponse
            {
                Success = false,
                Errors = new[] { "An error has occurred while updating the Offer." }
            });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            bool result = await mediator.Send(new DeleteOfferCommand() { Id = id });

            return result ? StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Success = true,
                SuccessMessage = "Offer deleted successfully."
            }) : BadRequest(new ApiResponse
            {
                Success = false,
                Errors = new[] { "An error has occurred while deleting the Offer." }
            });
        }

        [HttpGet("details-by-categories")]
        public async Task<IActionResult> GetDetailsByCategories()
        {
            var response = _offerService.GetDetailByCategories();

            return Ok(OfferViewDetail.From(response));
        }
    }
}
