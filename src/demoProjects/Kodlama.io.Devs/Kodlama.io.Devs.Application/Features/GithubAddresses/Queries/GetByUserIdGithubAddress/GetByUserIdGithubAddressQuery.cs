using AutoMapper;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Dtos;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Queries.GetByUserIdGithubAddress
{
    public class GetByUserIdGithubAddressQuery : IRequest<GithubAddressGetByUserIdDto>
    {
        public int UserId { get; set; }
        public class GetByUserIdGithubAddressQueryHandler : IRequestHandler<GetByUserIdGithubAddressQuery, GithubAddressGetByUserIdDto>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;
            private readonly GithubAddressBusinessRules _githubAddressBusinessRules;

            public GetByUserIdGithubAddressQueryHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper, GithubAddressBusinessRules githubAddressBusinessRules)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
                _githubAddressBusinessRules = githubAddressBusinessRules;
            }

            public async Task<GithubAddressGetByUserIdDto> Handle(GetByUserIdGithubAddressQuery request, CancellationToken cancellationToken)
            {
                GithubAddress? githubAddress = await _githubAddressRepository.GetAsync(b => b.UserId == request.UserId);

                _githubAddressBusinessRules.GithubAddressShouldExistWhenRequested(githubAddress);

                GithubAddressGetByUserIdDto githubAddressGetByUserIdDto = _mapper.Map<GithubAddressGetByUserIdDto>(githubAddress);
                return githubAddressGetByUserIdDto;
            }
        }
    }
}
