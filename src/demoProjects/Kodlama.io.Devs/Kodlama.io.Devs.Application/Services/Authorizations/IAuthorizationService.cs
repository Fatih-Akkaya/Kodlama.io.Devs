using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Services.Authorizations
{
    public interface IAuthorizationService
    {
        Task<AccessToken> CreateAccessToken(User user);
    }
}
