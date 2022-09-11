using AutoMapper;
using Core.Security.Dtos;
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

namespace Kodlama.io.Devs.Application.Features.Authorizations.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly IAuthorizationService _authorizationService;
            private readonly AuthorizationBusinessRules _authorizationBusinessRules;

            public RegisterCommandHandler(IUserRepository userRepository, IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository, IMapper mapper, IAuthorizationService authorizationService, AuthorizationBusinessRules authorizationBusinessRules)
            {
                _userRepository = userRepository;
                _userOperationClaimRepository = userOperationClaimRepository;
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _authorizationService = authorizationService;
                _authorizationBusinessRules = authorizationBusinessRules;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authorizationBusinessRules.UserEmailCanNotBeDuplicatedWhenInserted(request.Email);
                byte[] passWordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passWordHash, out passwordSalt);
                User mappedUser = _mapper.Map<User>(request);
                mappedUser.PasswordHash = passWordHash;
                mappedUser.PasswordSalt = passwordSalt;
                mappedUser.Status = true;
                User createdUser = await _userRepository.AddAsync(mappedUser);
                OperationClaim operationClaim = await _operationClaimRepository.GetAsync(o => o.Name == "User");
                UserOperationClaim userOperationClaim = new() { UserId = createdUser.Id, OperationClaimId = operationClaim.Id };
                await _userOperationClaimRepository.AddAsync(userOperationClaim);

                AccessToken createdAccessToken = await _authorizationService.CreateAccessToken(createdUser);
                RegisteredDto registeredDto = new RegisteredDto
                {
                    AccessToken = createdAccessToken
                };
                return registeredDto;
            }
        }

    }
}
