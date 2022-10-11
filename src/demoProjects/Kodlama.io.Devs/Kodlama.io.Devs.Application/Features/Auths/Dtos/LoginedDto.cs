using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Dtos
{
    public class LoginedDto
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }

        public LoginedResponseDto CreateResponseDto()
        {
            return new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType };
        }


        public class LoginedResponseDto
        {
            public AccessToken? AccessToken { get; set; }
            public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        }
    }
}
