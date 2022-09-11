using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Rules
{
    public class GithubAddressBusinessRules
    {
        private readonly IGithubAddressRepository _githubAddressRepository;

        public GithubAddressBusinessRules(IGithubAddressRepository githubAddressRepository)
        {
            _githubAddressRepository = githubAddressRepository;
        }
        public async Task GithubAddressCanNotBeDuplicatedWhenInserted(string githubUrl)
        {
            IPaginate<GithubAddress> result = await _githubAddressRepository.GetListAsync(b => b.GithubUrl == githubUrl);
            if (result.Items.Any()) throw new BusinessException("Github Address already exists.");
        }
        public async Task GithubAddressUserCanNotBeDuplicatedWhenInserted(int? userId = 0)
        {
            IPaginate<GithubAddress> result = await _githubAddressRepository.GetListAsync(b => b.UserId == userId);
            if (result.Items.Any()) throw new BusinessException("User already has Github Address.");
        }

        public void GithubAddressShouldExistWhenRequested(GithubAddress githubAddress)
        {
            if (githubAddress == null) throw new BusinessException("Request Github Address does not exist.");
        }
        public void GithubAddresNotMacthedRequestUser()
        {
            throw new BusinessException("You don't have permission to update Request Github Address.");
        }
    }
}
