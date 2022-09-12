using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.CreateGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.DeleteGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.UpdateGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Dtos;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Models;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Queries.GetByUserIdGithubAddress;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Queries.GetListGithubAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubAddressesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateGithubAddressCommand createGithubAddressCommand)
        {
            CreatedGithubAddressDto result = await Mediator.Send(createGithubAddressCommand);
            return Created("", result);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGithubAddressCommand updateGithubAddressCommand)
        {
            UpdatedGithubAddressDto result = await Mediator.Send(updateGithubAddressCommand);
            return Ok(result);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubAddressCommand deleteGithubAddressCommand)
        {
            DeletedGithubAddressDto result = await Mediator.Send(deleteGithubAddressCommand);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubAddressQuery getListGithubAddressQuery = new() { PageRequest = pageRequest };
            GithubAddressListModel result = await Mediator.Send(getListGithubAddressQuery);
            return Ok(result);
        }
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetById([FromRoute] GetByUserIdGithubAddressQuery getByUserIdGithubAddressQuery)
        {
            GithubAddressGetByUserIdDto githubAddressGetByUserIdDto = await Mediator.Send(getByUserIdGithubAddressQuery);
            return Ok(githubAddressGetByUserIdDto);
        }
    }
}
