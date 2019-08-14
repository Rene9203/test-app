using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Infraestructure;

namespace TestApp.Controllers
{
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected IMediator mediator;
        protected IUserInSession usserInSession;
        protected BaseController(IMediator mediator, IUserInSession userInSession)
        {
            this.mediator = mediator;
            this.usserInSession = userInSession;
        }
    }
}
