using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Dtos;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.UpdateGithubAddress
{
    public class UpdateGithubAddressCommand : IRequest<UpdatedGithubAddressDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string GithubUrl { get; set; }

        public string[] Roles => new string[] { "Admin", "User" };
        public class UpdateGithubAddressCommandHandler : IRequestHandler<UpdateGithubAddressCommand, UpdatedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UpdateGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules, IHttpContextAccessor httpContextAccessor)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<UpdatedGithubAddressDto> Handle(UpdateGithubAddressCommand request, CancellationToken cancellationToken)
            {
                if (!_httpContextAccessor.HttpContext.User.IsInRole("Admin") &&
                    request.UserId != int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value))
                    _githubAddressBusinessRules.GithubAddresNotMacthedRequestUser();
                GithubAddress? githubAddress = await _githubAddressRepository.GetAsync(b => b.Id == request.Id);
                _githubAddressBusinessRules.GithubAddressShouldExistWhenRequested(githubAddress);
                await _githubAddressBusinessRules.GithubAddressCanNotBeDuplicatedWhenInserted(request.GithubUrl);
                githubAddress.GithubUrl = request.GithubUrl;
                GithubAddress updatedGithubAddress = await _githubAddressRepository.UpdateAsync(githubAddress);
                UpdatedGithubAddressDto updatedGithubAddressDto = _mapper.Map<UpdatedGithubAddressDto>(updatedGithubAddress);

                return updatedGithubAddressDto;
            }           
        }
    }
}
