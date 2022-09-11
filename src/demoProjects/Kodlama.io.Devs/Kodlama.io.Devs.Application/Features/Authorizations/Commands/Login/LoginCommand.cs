using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Authorizations.Dtos;
using Kodlama.io.Devs.Application.Features.Authorizations.Rules;
using Kodlama.io.Devs.Application.Services.Authorizations;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Authorizations.Commands.Login
{
    public class LoginCommand : IRequest<LoginedDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthorizationService _authorizationService;
            private readonly AuthorizationBusinessRules _authorizationBusinessRules;

            public LoginCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService, AuthorizationBusinessRules authorizationBusinessRules)
            {
                _userRepository = userRepository;
                _authorizationService = authorizationService;
                _authorizationBusinessRules = authorizationBusinessRules;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? userForLogin = await _userRepository.GetAsync(b => b.Email == request.Email);
                _authorizationBusinessRules.UserEmailShouldExistWhenRequested(userForLogin);
                _authorizationBusinessRules.UserPasswordVerifiedWhenRequest(HashingHelper.VerifyPasswordHash(request.Password, userForLogin.PasswordHash, userForLogin.PasswordSalt));

                AccessToken createdAccessToken = await _authorizationService.CreateAccessToken(userForLogin);
                LoginedDto loginedDto = new LoginedDto
                {
                    AccessToken = createdAccessToken
                };
                return loginedDto;
            }
        }
    }
}
