using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Auths.Dtos;
using Kodlama.io.Devs.Application.Features.Auths.Rules;
using Kodlama.io.Devs.Application.Services.AuthService;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Application.Services.UserService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IPAddress { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {
            private readonly IUserService _userService;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules  _authBusinessRules;

            public LoginCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules  authBusinessRules)
            {
                _userService = userService;
                _authService = authService;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? userForLogin = await _userService.GetByEmail(request.UserForLoginDto.Email);
                _authBusinessRules.UserShouldExistWhenRequested(userForLogin);
                _authBusinessRules.UserPasswordVerifiedWhenRequest(userForLogin.Id, request.UserForLoginDto.Password);

                
                LoginedDto loginedDto = new LoginedDto();
                if (userForLogin.AuthenticatorType is not AuthenticatorType.None)
                {
                    if (request.UserForLoginDto.AuthenticatorCode is null)
                    {
                        await _authService.SendAuthenticatorCode(userForLogin);
                        loginedDto.RequiredAuthenticatorType = userForLogin.AuthenticatorType;
                        return loginedDto;
                    }

                    await _authService.VerifyAuthenticatorCode(userForLogin, request.UserForLoginDto.AuthenticatorCode);
                }

                AccessToken createdAccessToken = await _authService.CreateAccessToken(userForLogin);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(userForLogin, request.IPAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
                await _authService.DeleteOldRefreshTokens(userForLogin.Id);

                loginedDto.AccessToken = createdAccessToken;
                loginedDto.RefreshToken = addedRefreshToken;
                return loginedDto;
            }
        }
    }
}
