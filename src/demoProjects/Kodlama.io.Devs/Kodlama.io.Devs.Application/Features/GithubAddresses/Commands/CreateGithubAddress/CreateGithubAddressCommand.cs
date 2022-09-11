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

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.CreateGithubAddress
{
    public class CreateGithubAddressCommand : IRequest<CreatedGithubAddressDto>, ISecuredRequest
    {
        public int? UserId { get; set; }
        public string GithubUrl { get; set; }

        public string[] Roles => new string[] { "Admin", "User" };

        public class CreateGithubAddressCommandHandler : IRequestHandler<CreateGithubAddressCommand, CreatedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules, IHttpContextAccessor httpContextAccessor)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<CreatedGithubAddressDto> Handle(CreateGithubAddressCommand request, CancellationToken cancellationToken)
            {
                if(!_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                    request.UserId= int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                await _githubAddressBusinessRules.GithubAddressCanNotBeDuplicatedWhenInserted(request.GithubUrl);
                await _githubAddressBusinessRules.GithubAddressUserCanNotBeDuplicatedWhenInserted(request.UserId);
                
                GithubAddress mappedGithubAddress = _mapper.Map<GithubAddress>(request);
                GithubAddress createdGithubAddress = await _githubAddressRepository.AddAsync(mappedGithubAddress);
                CreatedGithubAddressDto createdGithubAddressDto = _mapper.Map<CreatedGithubAddressDto>(createdGithubAddress);

                return createdGithubAddressDto;
            }
        }
    }
}
