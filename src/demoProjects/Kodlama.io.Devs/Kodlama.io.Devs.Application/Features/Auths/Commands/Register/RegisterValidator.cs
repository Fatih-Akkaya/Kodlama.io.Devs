using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(c => c.UserForRegisterDto.Email).NotEmpty();
            RuleFor(c => c.UserForRegisterDto.Email).EmailAddress();
            RuleFor(c => c.UserForRegisterDto.Email).MinimumLength(5);
        }
    }
}
