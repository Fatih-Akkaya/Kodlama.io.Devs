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

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.DeleteGithubAddress
{
    public class DeleteGithubAddressCommand : IRequest<DeletedGithubAddressDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string GithubUrl { get; set; }

        public string[] Roles => new string[] { "Admin", "GithubDelete" };
        public class DeleteGithubAddressCommandHandler : IRequestHandler<DeleteGithubAddressCommand, DeletedGithubAddressDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;

            public DeleteGithubAddressCommandHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
            }

            public async Task<DeletedGithubAddressDto> Handle(DeleteGithubAddressCommand request, CancellationToken cancellationToken)
            {                                
                GithubAddress? githubAddress = await _githubAddressRepository.GetAsync(b => b.Id == request.Id && b.UserId == request.UserId);
                _githubAddressBusinessRules.GithubAddressShouldExistWhenRequested(githubAddress);
                
                GithubAddress deletedGithubAddress = await _githubAddressRepository.DeleteAsync(githubAddress);
                DeletedGithubAddressDto deletedGithubAddressDto = _mapper.Map<DeletedGithubAddressDto>(deletedGithubAddress);

                return deletedGithubAddressDto;
            }
        }
    }
}
