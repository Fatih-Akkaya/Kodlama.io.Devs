using Kodlama.io.Devs.Application.Features.Authorizations.Commands.Login;
using Kodlama.io.Devs.Application.Features.Authorizations.Commands.Register;
using Kodlama.io.Devs.Application.Features.Authorizations.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            RegisteredDto result = await Mediator.Send(registerCommand);
            return Created("", result.AccessToken);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            LoginedDto result = await Mediator.Send(loginCommand);
            return Ok(result.AccessToken);
        }
    }
}
