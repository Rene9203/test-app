using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestApp.Application.Exceptions;
using TestApp.Application.ViewModels;
using TestApp.Core.Infraestructure;
using TestApp.Identity;
using TestApp.Infraestructure;

namespace TestApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager,
            IMediator mediator,
            IUserInSession userInSession) : base(mediator, userInSession)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(loginViewModel.User);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginViewModel.Pass))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserId",user.Id.ToString())
                    }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("th3s3cr3tK3Yf0rApp*")), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    return Ok(new
                    {
                        token,
                        user = new
                        {
                            id = user.Id,
                            user = user.UserName,
                            roles = await _userManager.GetRolesAsync(user)
                        }
                    });
                }
                else
                    throw new ApiException("Please check the password", StatusCodes.Status400BadRequest);
            }

            throw new ApiException("Please check the email", StatusCodes.Status400BadRequest);
        }
    }
}