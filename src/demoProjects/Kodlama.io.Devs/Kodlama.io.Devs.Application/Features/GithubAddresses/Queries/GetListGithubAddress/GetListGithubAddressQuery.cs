using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Queries.GetListGithubAddress
{
    public class GetListGithubAddressQuery : IRequest<GithubAddressListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListGithubAddressQueryHandler : IRequestHandler<GetListGithubAddressQuery, GithubAddressListModel>
        {
            private readonly IGithubAddressRepository _githubAddressRepository;
            private readonly IMapper _mapper;

            public GetListGithubAddressQueryHandler(IGithubAddressRepository githubAddressRepository, IMapper mapper)
            {
                _githubAddressRepository = githubAddressRepository;
                _mapper = mapper;
            }

            public async Task<GithubAddressListModel> Handle(GetListGithubAddressQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GithubAddress> githubaddresses = await _githubAddressRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                GithubAddressListModel mappedGithubAddressListModel = _mapper.Map<GithubAddressListModel>(githubaddresses);
                return mappedGithubAddressListModel;
            }
        }
    }
}
