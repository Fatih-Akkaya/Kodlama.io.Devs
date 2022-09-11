using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Authorizations.Rules
{
    public class AuthorizationBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Items.Any()) throw new BusinessException("User e-Mail already exists.");
        }

        public void UserEmailShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("Request e-Mail does not exist.");
        }
        public void UserPasswordVerifiedWhenRequest(bool verify)
        {
            if (!verify) throw new BusinessException("Request password is not verified.");
        }
    }
}
