using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Authorizations.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Email).MinimumLength(5);
        }
    }
}
